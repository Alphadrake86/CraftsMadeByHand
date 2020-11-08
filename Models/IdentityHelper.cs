using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftsMadeByHand.Models
{
    
    public static class IdentityHelper
    {
        // Roles
        public const string BuyerRole = "Buyer";
        public const string SellerRole = "Seller";
        public const string AdminRole = "Admin";


        public static void SetIdentityOptions(IdentityOptions options)
        {
            // Sign In options
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;

            // Password options
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 8;
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach(string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        internal static async Task CreateDefaultAdministrator(IServiceProvider serviceProvider)
        {
            const string Username = "Admin";
            const string Password = "Admin";
            const string Email = "Admin@craftsmadebyhand.com";

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if(userManager.Users.Count() == 0)
            {
                IdentityUser Admin = new IdentityUser()
                {
                    Email = Email,
                    EmailConfirmed = true,
                    UserName = Username,

                };

                // Create Admin and add to role
                await userManager.CreateAsync(Admin, Password);
                await userManager.AddToRoleAsync(Admin, AdminRole);
            }
        }
    }
}
