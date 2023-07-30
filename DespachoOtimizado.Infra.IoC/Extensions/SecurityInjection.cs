using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DespachoOtimizado.Infra.IoC.Extensions
{
    public static class SecurityInjection
    {
        public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
                
                options.SaveToken = true;
            });

            // Cookies
            // services.ConfigureApplicationCookie(options => 
            //     options.AccessDeniedPath = "/Account/Login");
        }
        
    }
}