using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Contexts
{
    public static class DataInitialization
    {
        public static async Task SeedAsync(UserManager<User> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            if (!RoleManager.Roles.Any())
            {
                await RoleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
                await RoleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await RoleManager.CreateAsync(new IdentityRole() { Name = "Portero" });
                await RoleManager.CreateAsync(new IdentityRole() { Name = "Guard" });
            }

            if (await UserManager.FindByEmailAsync("admin@admin.com") == null)
            {
                var Result = await UserManager.CreateAsync(new User() { Email = "admin@admin.com", UserName = "admin@admin.com" }, "admin");

                if (Result.Succeeded)
                {
                    var User = await UserManager.FindByEmailAsync("admin@admin.com");
                    if (User != null) await UserManager.AddToRoleAsync(User, "SuperAdmin");
                } else
                {
                    Console.WriteLine(Result.Errors);
                }
            }
        }
    }
}
