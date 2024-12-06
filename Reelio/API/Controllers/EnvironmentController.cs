using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("environments")]
    public class EnvironmentController : Controller
    {
        private readonly IHostEnvironment _hostEnvironment;

        public EnvironmentController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Route("current")]
        public IActionResult GetEnvironment()
        {
            return Ok(new { Environment = _hostEnvironment.EnvironmentName });
        }
    }
}
