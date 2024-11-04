using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("Hello world with ideal-architecture");
        }
    }
}
