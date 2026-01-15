using Ayllu.Web.Application.Common.Abstractions.Antithesis;
using Ayllu.Web.Application.Common.Abstractions.Data;
using Ayllu.Web.Application.Common.Abstractions.Dialectic;
using Ayllu.Web.Application.Common.Abstractions.Email;
using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Common.Abstractions.Identity;
using Ayllu.Web.Application.Common.Abstractions.Storage;
using Ayllu.Web.Application.Common.Abstractions.Synthesis;
using Ayllu.Web.Application.Common.Abstractions.Thesis;
using Ayllu.Web.Application.Common.Abstractions.UnitOfWork;
using Ayllu.Web.Application.Common.Behaviours;
using Ayllu.Web.Application.Common.Mappings;
using Ayllu.Web.Application.Identity.Queries;
using Ayllu.Web.Application.Identity.Validators;
using Ayllu.Web.Domain.Entities.Identity;
using Ayllu.Web.Infrastructure.Common.Factories.Jobs;
using Ayllu.Web.Infrastructure.Common.Repositories.Common;
using Ayllu.Web.Infrastructure.Common.Repositories.Identity;
using Ayllu.Web.Infrastructure.Common.Services.Antithesis;
using Ayllu.Web.Infrastructure.Common.Services.Dialectic;
using Ayllu.Web.Infrastructure.Common.Services.Health;
using Ayllu.Web.Infrastructure.Common.Services.Identity;
using Ayllu.Web.Infrastructure.Common.Services.Synthesis;
using Ayllu.Web.Infrastructure.Common.Services.Thesis;
using Ayllu.Web.Infrastructure.Common.UnitOfWork;
using Ayllu.Web.Infrastructure.Communication.Email.Smtp;
using Ayllu.Web.Infrastructure.Communication.Email.Templates;
using Ayllu.Web.Infrastructure.Persistence.Data;
using Ayllu.Web.Infrastructure.Persistence.Extensions;
using Ayllu.Web.Infrastructure.Persistence.Options;
using Ayllu.Web.Infrastructure.Persistence.Utils;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace Ayllu.Web.Composition;


/// <summary>
/// AylluModule is a static class that provides extension methods to register the Ayllu module in a web application.
/// </summary>
public static class AylluModule
{
    /// <summary>
    /// Registers the Ayllu module in the provided service collection.
    /// </summary>
    /// <param name="services">Colleciont of services from the builder</param>
    /// <param name="configuration">Instance of configuration present in the builder</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddAylluModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureLayer(configuration);
        services.AddApplicationLayer();
        return services;
    }

    /// <summary>
    /// Adds the application layer services to the service collection.
    /// </summary>
    /// <param name="services">The collection of services from DI container</param> 
    /// <returns>The service collection with the Application Layer set uo</returns>
    private static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<LogoutQuery>());
        services.AddAutoMapper(am => am.AddMaps(typeof(AylluMappingProfile).Assembly));
        services.AddValidatorsFromAssembly(typeof(GetCurrentUserQueryValidator).Assembly, includeInternalTypes: true);
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );
        return services;
    }

    /// <summary>
    /// Adds the infrastructure layer services to the service collection.
    /// </summary>
    /// <param name="services">The collection of services from DI Container</param>
    /// <param name="configuration">The instance of <see cref="IConfiguration"/> with the settings for the app</param>
    /// <returns>The <see cref="IServiceCollection"/> instance with the setup of configurations and settings for services</returns>
    /// <exception cref="InvalidOperationException">Throws when the connections string are missing</exception>
    private static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(
           configuration.GetSection(ConnectionStringsOptions.SectionName));

        services.AddOptions<ConnectionStringsOptions>()
            .Bind(configuration.GetSection(ConnectionStringsOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(o => !string.IsNullOrEmpty(o.DefaultConnection), "Default Connection is a required connection string")
            .ValidateOnStart();

        services.Configure<GoogleSmtpOptions>(
           configuration.GetSection(GoogleSmtpOptions.SectionName));

        services.AddOptions<GoogleSmtpOptions>()
            .Bind(configuration.GetSection(GoogleSmtpOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(o => !string.IsNullOrWhiteSpace(o.Password), "SMTP password is required")
            .Validate(o => !string.IsNullOrWhiteSpace(o.FromEmail), "SMTP email is required")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Host), "SMTP host is required")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Port.ToString()), "SMTP port is required")
            .ValidateOnStart();
        services.AddAuthentication();
        services.AddAuthorization(options =>
        {
            //options.AddPolicy("ScalarPolicy", policy =>
            //{
            //    policy.RequireAssertion(context =>
            //    {
            //        var httpContext = context.Resource as HttpContext;
            //        var token = httpContext?.Request.Headers["Authorization"].FirstOrDefault();
            //        return token == $"Bearer {configuration["Scalar:Token:Bearer"]}";
            //    });
            //});
        });

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

        // Configura Identity
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
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@._-";
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddApiEndpoints()
        .AddSignInManager<SignInManager<ApplicationUser>>();

        // services
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
        // Quartz
        //services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        //services.AddSingleton<IJobFactory, ScopedJobFactory>();
        //services.AddQuartz(q =>
        //{
        //    // Define o tipo de serializador
        //    q.SetProperty("quartz.serializer.type", "json");

        //    q.UsePersistentStore(store =>
        //    {
        //        store.UseProperties = true;
        //        store.UseSqlServer(sql =>
        //        {
        //            sql.ConnectionString = configuration.GetConnectionString("QuartzConnection") ?? throw new InvalidOperationException("Missing Connection string ConnectionStrings:QuartzConnection");
        //            sql.TablePrefix = "QRTZ_";
        //        });
        //    });

        //    q.UseJobFactory<ScopedJobFactory>();

        //    // Cleanup expired JwtTokens
        //    // JobKey cleanupJobKey = new("TokenCleanupJob");
        //    // q.AddJob<TokenCleanupJob>(opts => opts.WithIdentity(cleanupJobKey));
        //    // q.AddTrigger(opts => opts
        //    //     .ForJob(cleanupJobKey)
        //    //     .WithIdentity("TokenCleanupJob-trigger")
        //    //     .WithCronSchedule("0 0 0 * * ?")); // 00:00 UTC
        //});
        return services;
    }

    public static async Task Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
        try
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedMainUserAsync(userManager, roleManager);
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    private static async Task SeedMainUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        const string mainUserEmail = "aylludev@gmail.com";
        const string mainUserName = "ayllu";
        const string mainPassword = "4yllU@4YLLu_";

        // 1️⃣ Roles
        var roles = new[]
        {
            "ApplicationAdmin",
            "ApplicationUser",
            "GroupAdmin",
            "GroupUser",
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole(role));
            }
        }

        // 2️⃣ Usuário
        var user = await userManager.FindByEmailAsync(mainUserEmail);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = mainUserName,
                Email = mainUserEmail,
                EmailConfirmed = true,
                Name = "Ayllu",
                LastName = "Application",
                Culture = "pt-BR"
            };

            var createResult = await userManager.CreateAsync(user, mainPassword);
            if (!createResult.Succeeded)
            {
                throw new Exception(
                    $"Failed to create main user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }
        }

        // 3️⃣ Roles do usuário
        var userRoles = await userManager.GetRolesAsync(user);
        var missingRoles = roles.Except(userRoles);

        if (missingRoles.Any())
        {
            await userManager.AddToRolesAsync(user, missingRoles);
        }
    }
}
