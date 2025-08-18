using Ayllu.Application.Common.Interfaces;
using Ayllu.Application.CQRS.Commands;
using Ayllu.Application.Services;
using Ayllu.Domain.Entities;
using Ayllu.Infrastructure.Data;
using Ayllu.Infrastructure.Repositories;
using Ayllu.Infrastructure.Services; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ayllu.Composition
{
    public static class AylluComposition
    {
        public static IServiceCollection AddAyllu(this IServiceCollection services, IConfiguration configuration)
        {
            // ---------------------------
            // 1. Configuração do DbContext
            // ---------------------------
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ---------------------------
            // 2. Configuração do Identity
            // ---------------------------
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


            // ---------------------------
            // 3. TokenGenerator
            // ---------------------------
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            // ---------------------------
            // 4. Repositories / Interfaces
            // ---------------------------
            services.AddScoped<IOrganizationRolesRepository, OrganizationRolesRepository>();

            // ---------------------------
            // 5. IdentityService (Application)
            // ---------------------------
            services.AddScoped<IIdentityService, IdentityService>();
            
            // ---------------------------
            // 6. MediatR
            // ---------------------------
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(RegisterCommand).Assembly // Application
                );
            });

            // ---------------------------
            // 7. Configurações adicionais, se houver
            // ---------------------------
            // ex: services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}