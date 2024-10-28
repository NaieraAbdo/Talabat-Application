namespace Talabat.APIs.Test1.Errors
{
    public class ApiExceptionResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int statusCode , string? message=null, string? details=null):base(statusCode,message)
        {
            Details = details;
        }
    }
}
