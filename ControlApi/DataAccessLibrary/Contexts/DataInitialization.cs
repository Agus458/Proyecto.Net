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
            if (!await RoleManager.RoleExistsAsync("SuperAdmin"))
            {
                await RoleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
            }

            if (!await RoleManager.RoleExistsAsync("Admin"))
            {
                await RoleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
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
