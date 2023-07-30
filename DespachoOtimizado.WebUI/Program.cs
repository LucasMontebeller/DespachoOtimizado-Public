using System.Net;
using DespachoOtimizado.Domain.Interfaces.Account;
using DespachoOtimizado.Infra.IoC.Extensions;
namespace DespachoOtimizado.WebUI
{

    public class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddSecurity(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Redirect por statusCode
            app.UseStatusCodePages(async context =>
            {
                var statusCode = context.HttpContext.Response.StatusCode;
                switch (statusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                        context.HttpContext.Response.Redirect("/Error/Unauthorized");
                        break;

                    case 500:
                        context.HttpContext.Response.Redirect("/Error/ServerError");
                        break;
                }
            });
            app.UseExceptionHandler("/Error/ServerError");
            app.UseRouting();

            // Cria as roles e usuários pré definidos
            var seedUserService = builder.Services.BuildServiceProvider().GetService<ISeedUserRoleService>();
            await seedUserService.SeedRoles();
            await seedUserService.SeedUsers();

            // app.UseMiddleware<TokenExpirationMiddleware>(); // validar se será necessário
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}