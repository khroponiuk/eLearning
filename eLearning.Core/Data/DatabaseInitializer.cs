using eLearning.Core.Entities;
using eLearning.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eLearning.Core.Data
{
    public class DatabaseInitializer
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            await CreateRoles(serviceProvider, configuration);
        }

        static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleNames = Enum.GetNames(typeof(UserRoles));

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var email = configuration.GetSection("UserSettings")["UserEmail"];
            var password = configuration.GetSection("UserSettings")["UserPassword"];
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var createPowerUser = await userManager.CreateAsync(adminUser, password);

                if (createPowerUser.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
