using Microsoft.AspNetCore.Identity;

namespace F1_Car_Garage.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Create roles
            string[] roles = { "Admin", "Racer", "Manufacturer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Users to create: username, email, password, role
            var users = new[]
            {
                new { UserName = "admin", Email = "admin@f1garage.com", Password = "admin", Role = "Admin" },
                new { UserName = "legend", Email = "legend@f1garage.com", Password = "legend", Role = "Racer" },
                new { UserName = "pitstop", Email = "pitstop@f1garage.com", Password = "pitstop", Role = "Manufacturer" }
            };

            foreach (var u in users)
            {
                var existingByName = await userManager.FindByNameAsync(u.UserName);
                IdentityUser userToUse = null;

                if (existingByName == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = u.UserName,
                        Email = u.Email,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(user, u.Password);
                    if (result.Succeeded)
                    {
                        userToUse = user;
                    }
                    else
                    {
                        // If creation failed, try to find by email as fallback untested, may switch to username instead of email as the unique identifier if this causes issues
                        userToUse = await userManager.FindByEmailAsync(u.Email);
                    }
                }
                else
                {
                    userToUse = existingByName;
                }

                if (userToUse != null)
                {
                    if (!await userManager.IsInRoleAsync(userToUse, u.Role))
                        await userManager.AddToRoleAsync(userToUse, u.Role);
                }
            }
        }
    }
}
