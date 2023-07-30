using DespachoOtimizado.Domain.Interfaces.Account;
using DespachoOtimizado.Infra.Data.Context;
using DespachoOtimizado.Infra.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DespachoOtimizado.Application;
using DespachoOtimizado.Application.Services;

namespace DespachoOtimizado.Infra.IoC.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration.GetConnectionString("LocalHost");

            services.AddDbContext<ApplicationDbContext>
            (      
                options => options.UseSqlServer(defaultConnection,
                action => action.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            });

            // Injeção dos serviços e repositories
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ISeedUserRoleService, SeedUserRoleService>();
            services.AddScoped<IOptimizerService, OptimizerService>();
            
            return services;
        }
    }
}