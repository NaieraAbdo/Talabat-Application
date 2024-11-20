using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.APIs.Test1.Errors;
using Talabat.APIs.Test1.Extensions;
using Talabat.APIs.Test1.Helper;
using Talabat.APIs.Test1.MiddleWares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;
using Talabat.Repository.Identity;

namespace Talabat.APIs.Test1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /* StoreContext dbContext = new StoreContext();
            dbContext.Database.MigrateAsync();*/   //error occuring cause of the ctor of StoreContext

            var WebApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the DI container.

            WebApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            WebApplicationBuilder.Services.AddEndpointsApiExplorer();
            WebApplicationBuilder.Services.AddSwaggerGen();

            WebApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            }
                );

            WebApplicationBuilder.Services.AddApplicationServices();
            WebApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var connection = WebApplicationBuilder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            }
            );

            WebApplicationBuilder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });

            WebApplicationBuilder.Services.AddIdentityServices(WebApplicationBuilder.Configuration);

            #endregion

            var app = WebApplicationBuilder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            //Ask CLR for creating obj from DbContext Explicitely

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();  //Update Databases
                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                //Data Seeding
                var userManager = services.GetRequiredService<UserManager<AppUser>>(); 
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }

            #region Configure Kestral

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            #endregion


            app.Run();
        }
    }
}