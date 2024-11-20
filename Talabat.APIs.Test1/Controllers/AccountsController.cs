using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Test1.DTOs;
using Talabat.APIs.Test1.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Test1.Controllers
{
    
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser() { 
                DisplayName = model.DisplayName,
                UserName = model.Email.Split('@')[0],
                Email =model.Email,
                PhoneNumber =model.PhoneNumber};
            var Result = await userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            var ReturnedUser = new UserDto()
            { DisplayName = model.DisplayName,
                Email = model.Email,
                Token =await tokenService.CreateTokenAsync(user, userManager)
            };
            return Ok(ReturnedUser);
        }

        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var Result = await signInManager.CheckPasswordSignInAsync(user,model.Password,false);
            if(!Result.Succeeded)return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto(){
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }
    }
}
