using BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Common.DTO;
using Azure;
using BLL.Services;
using Microsoft.AspNetCore.Identity.Data;
namespace API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }
        // GET: UserController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: account
        [HttpGet]
        [Route("/account")]
        public UserJWTDTO? GetAccount()
        {
            return HttpContext.Items["User"] as UserJWTDTO;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO body)
        {
            if (body == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            var response = await userService.Authenticate(body);

            if (response.Message == "Invalid credentials")
            {
                return Unauthorized(new { message = response.Message });
            }

            return Ok(new { message = response.Message, token = response.Token });

        }


        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserDTO model)
        {
            UserDTO userDTO = new UserDTO
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
            
            if (userDTO == null)
            {
                return BadRequest("Invalid user data.");
            }

            string? token = await userService.RegisterUser(model);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("User registration failed or token generation error.");
            }

            return Ok(new
            {
                message = "Registration successful",
                token
            });
        }
    }
}
