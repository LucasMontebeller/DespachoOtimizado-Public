using System.Security.Claims;
using DespachoOtimizado.Domain.Interfaces.Account;

namespace DespachoOtimizado.WebUI.Middlewares
{
    public class TokenExpirationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string LoginPath = "/Account/Login";
        public TokenExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.Equals(LoginPath))
            {
                using (var scope = context.RequestServices.CreateScope())
                {
                    var authenticateService = scope.ServiceProvider.GetRequiredService<IAuthenticateService>();

                    if (!context.User.Identity.IsAuthenticated || IsTokenExpired(context.User.FindFirst("Expiration")))
                    {
                        await authenticateService.Logout();
                        context.Response.Redirect(LoginPath);
                        return;
                    }
                }
            }

            await _next(context);
        }

        private bool IsTokenExpired(Claim claim)
        {
            if (claim == null)
                return true;

            if (DateTime.TryParse(claim.Value, out var expirationDate) && expirationDate <= DateTime.UtcNow)
                return true;

            return false;
        }
    }
}