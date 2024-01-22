using EntityLayer.Entities;
using EntityLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        string adminRole = UserRole.Admin.ToString();
        string userRole = UserRole.User.ToString();
        foreach (var roleName in new[] { adminRole, userRole })
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    throw new ApplicationException($"Rol oluşturulamadı: {roleName}");
                }
            }
        }

        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            var newAdminUser = new AppUser
            {
                UserName = "admin1",
                Email = "admin@example.com",
                PhoneNumber = "11111111",
                Role = UserRole.Admin
            };

            await userManager.CreateAsync(newAdminUser, "Admin1.");
            await userManager.AddToRoleAsync(newAdminUser, adminRole);
        }
    }
}