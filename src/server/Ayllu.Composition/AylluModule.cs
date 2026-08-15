using Ayllu.Application.Common.Abstractions.Antithesis;
using Ayllu.Application.Common.Abstractions.Data;
using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Common.Abstractions.Email;
using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Common.Abstractions.Storage;
using Ayllu.Application.Common.Abstractions.Synthesis;
using Ayllu.Application.Common.Abstractions.Thesis;
using Ayllu.Application.Common.Abstractions.UnitOfWork;
using Ayllu.Application.Common.Mediator.Extensions;
using Ayllu.Application.Common.Mediator.Middlewares;
using Ayllu.Application.Dialectics.Commands;
using Ayllu.Application.Identity.Queries;
using Ayllu.Application.Identity.Validators;
using Ayllu.Domain.Entities.Identity;
using Ayllu.Infrastructure.Common.Repositories.Common;
using Ayllu.Infrastructure.Common.Repositories.Identity;
using Ayllu.Infrastructure.Common.Services.Antithesis;
using Ayllu.Infrastructure.Common.Services.Dialectic;
using Ayllu.Infrastructure.Common.Services.Health;
using Ayllu.Infrastructure.Common.Services.Identity;
using Ayllu.Infrastructure.Common.Services.Synthesis;
using Ayllu.Infrastructure.Common.Services.Thesis;
using Ayllu.Infrastructure.Common.UnitOfWork;
using Ayllu.Infrastructure.Communication.Email.Smtp;
using Ayllu.Infrastructure.Communication.Email.Templates;
using Ayllu.Infrastructure.Persistence.Data;
using Ayllu.Infrastructure.Persistence.Options;
using Ayllu.Infrastructure.Persistence.Utils;
using FluentValidation;
using Mediator.Net;
using Mediator.Net.MicrosoftDependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Ayllu.Composition;

/// <summary>
/// Registers the Ayllu application, infrastructure and mediator services.
/// </summary>
public static class AylluModule
{
    /// <summary>
    /// Registers the Ayllu module in the provided service collection.
    /// </summary>
    public static IServiceCollection AddAylluModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureLayer(configuration);
        services.AddApplicationLayer();
        return services;
    }

    private static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(GetCurrentUserQueryValidator).Assembly, includeInternalTypes: true);
        var builder = new MediatorBuilder();
        builder.RegisterHandlers(typeof(CreateDialecticCommand).Assembly);
        builder.ConfigureGlobalReceivePipe(pipe => pipe.UseLogging());
        builder.ConfigureCommandReceivePipe(pipe => pipe.UseFluentValidation());
        services.RegisterMediator(builder);
        return services;
    }

    private static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
        services.AddOptions<ConnectionStringsOptions>()
            .Bind(configuration.GetSection(ConnectionStringsOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(o => !string.IsNullOrEmpty(o.DefaultConnection), "Default Connection is a required connection string")
            .ValidateOnStart();
        services.Configure<GoogleSmtpOptions>(configuration.GetSection(GoogleSmtpOptions.SectionName));
        services.AddOptions<GoogleSmtpOptions>()
            .Bind(configuration.GetSection(GoogleSmtpOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(o => !string.IsNullOrWhiteSpace(o.Password), "SMTP password is required")
            .Validate(o => !string.IsNullOrWhiteSpace(o.FromEmail), "SMTP email is required")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Host), "SMTP host is required")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Port.ToString()), "SMTP port is required")
            .ValidateOnStart();
        services.AddAuthentication();
        services.AddAuthorization();
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Missing Connection string ConnectionStrings:DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, dbOptions =>
            {
                dbOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly);
                dbOptions.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
                dbOptions.EnableRetryOnFailure();
                dbOptions.CommandTimeout(90);
            });
        });
        services.AddSingleton<IEmailTemplateRenderer, EmailTemplateRenderer>();
        services.AddSingleton<IEmailSender<ApplicationUser>, GoogleSmtpEmailSender>();
        services.AddHttpContextAccessor();
        services.AddIdentityApiEndpoints<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@._-";
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddApiEndpoints()
        .AddSignInManager<SignInManager<ApplicationUser>>();
        services.AddScoped<IApplicationDbMigrator, ApplicationDbMigrator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IApplicationUserFriendshipRepository, ApplicationUserFriendshipRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IHealthService, HealthService>();
        services.AddScoped<IAntithesisService, AntithesisService>();
        services.AddScoped<IDialecticService, DialecticService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ISynthesisService, SynthesisService>();
        services.AddScoped<IThesisService, ThesisService>();
        return services;
    }
}
