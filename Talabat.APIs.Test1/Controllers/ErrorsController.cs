using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Test1.Errors;

namespace Talabat.APIs.Test1.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error (int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
