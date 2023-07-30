using Microsoft.AspNetCore.Identity;

namespace DespachoOtimizado.Infra.Data.Account
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
    }
}