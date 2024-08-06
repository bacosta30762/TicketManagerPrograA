using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Seeders
{
    public class AdminSeeder
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(roleName));
                }
            }

            var user = await userManager.FindByEmailAsync("admin@example.com");

            if (user == null)
            {
                user = new User()
                {
                    Name = "Admin",
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                };
                await userManager.CreateAsync(user, "Admin@123");
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
