using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Test1.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var Email = user.FindFirstValue(ClaimTypes.Email);
            var User = await userManager.Users.Include(U =>U.Address).FirstOrDefaultAsync(U =>U.Email ==Email);
            return User;
        }
    }
}
