using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftsMadeByHand.Models
{
    public static class IdentityHelper
    {
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
    }
}
