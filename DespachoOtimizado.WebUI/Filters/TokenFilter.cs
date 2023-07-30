using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace DespachoOtimizado.WebUI.Filters
{
    public class TokenValidationFilter : IAuthorizationFilter
    {

        private readonly IConfiguration _configuration;
        public TokenValidationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
                
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                context.HttpContext.User = principal;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}