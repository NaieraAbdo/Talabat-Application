using System.Text.Json;
using Talabat.APIs.Test1.Errors;

namespace Talabat.APIs.Test1.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment enviro;

        public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger,IHostEnvironment enviro)
        {
            this.next = next;
            this.logger = logger;
            this.enviro = enviro;
        }

        //Invoke Async Func
        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;//or (int) httpStatusCode.internalServerError

                var Response = enviro.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse(500);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize(Response);
                context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
