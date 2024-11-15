﻿using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Test1.Errors;
using Talabat.APIs.Test1.Helper;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIs.Test1.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    //Model State => Dic [key=>name of prop],[val=>error]
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse() { Errors = errors };
                    return new BadRequestObjectResult(ValidationErrorResponse);

                };
            });

            return services;
        }
    }
}
