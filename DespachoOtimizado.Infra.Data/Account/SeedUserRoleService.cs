using DespachoOtimizado.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace DespachoOtimizado.Infra.Data.Account
{
    public class SeedUserRoleService : ISeedUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SeedUserRoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedUsers()
        {
            // Jogar para viewModel
            if (await _userManager.FindByEmailAsync("user@mogai.com") is null)
            {
                var applicationUser = new ApplicationUser()
                {
                    Email = "user@mogai.com",
                    UserName = "user",
                    NormalizedEmail = "USER@MOGAI.COM",
                    NormalizedUserName = "USER",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(applicationUser, "Mog@ai#123");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(applicationUser, "User");
            }

            if (await _userManager.FindByEmailAsync("admin@mogai.com") is null)
            {
                var applicationUser = new ApplicationUser()
                {
                    Email = "admin@mogai.com",
                    UserName = "admin",
                    NormalizedEmail = "ADMIN@MOGAI.COM",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(applicationUser, "Mog@ai#123456");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(applicationUser, "Admin");
            }
        }

        public async Task SeedRoles()
        {
            // Jogar para viewModel
            
            if (await _roleManager.FindByNameAsync("User") is null)
            {
                var role = new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER"
                };
                
                await _roleManager.CreateAsync(role);
            }
            if (await _roleManager.FindByNameAsync("Admin") is null)
            {
                var role = new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                
                await _roleManager.CreateAsync(role);
            }
        }
    }
}