using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DespachoOtimizado.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DespachoOtimizado.Infra.Data.Account
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILogger<AuthenticateService> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticateService(
            ILogger<AuthenticateService> logger,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager
            )
        {
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!result.Succeeded)
                return false;
            
            try
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.AccessTokenExpirationTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                await _userManager.UpdateAsync(user);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao gerar token para o usuário {user.UserName}. Erro : {ex.Message}");
                return false;
            }

        }

        public async Task<Tuple<bool, IEnumerable<string>>> RegisterUser(string name, string email, string phone, string password)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = name,
                Email = email,
                PhoneNumber = phone
            };
            var result = await _userManager.CreateAsync(applicationUser, password);
            var errors = result.Errors.Select(e => e.Description);

            return new (result.Succeeded, errors);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        } 

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<bool> ExistsUser(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result is null ? false : true;
        }

    }
}