
using API.Error;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : BaseApiController
    {
        [Route("api/[controller]")]
        [ApiExplorerSettings(IgnoreApi =true)]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        } 
    }
}