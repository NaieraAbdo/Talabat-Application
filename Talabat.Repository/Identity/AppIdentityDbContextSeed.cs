using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "noor",
                    Email = "noorabdo12@gmail.com",
                    UserName = "naieranoor",
                    PhoneNumber = "0100123511"
                };
                await userManager.CreateAsync(User, "pa$$word");
            }

        }
    }
}
