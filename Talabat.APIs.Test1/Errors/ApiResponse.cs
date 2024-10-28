namespace Talabat.APIs.Test1.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode,string?message =null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "BadRequest",
                401 => "You'reNotAutherized",
                404 => "ResourceNotFound",
                500 => "InternalServerError",
                _ => null
            };
        }
    }
}
