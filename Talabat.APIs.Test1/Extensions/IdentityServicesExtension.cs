using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Test1.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<AppIdentityDbContext>();
            
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                      .AddJwtBearer(Options => {
                          Options.TokenValidationParameters = new TokenValidationParameters()
                          {
                              ValidateIssuer = true,
                              ValidIssuer = configuration["JWT:ValidIssuer"],
                              ValidateAudience =true,
                              ValidAudience = configuration["JWT:ValidAudience"],
                              ValidateLifetime =true,
                              ValidateIssuerSigningKey =true,
                              IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                          };
                      });
            return services;
        }
    }
}
