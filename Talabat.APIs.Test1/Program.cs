using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.APIs.Test1.Errors;
using Talabat.APIs.Test1.Helper;
using Talabat.APIs.Test1.MiddleWares;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;

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

            WebApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            WebApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));
            WebApplicationBuilder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    //Model State => Dic [key=>name of prop],[val=>error]
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count()> 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse() { Errors = errors };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                    
                };
            }); 

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
                //Data Seeding
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

            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion


            app.Run();
        }
    }
}