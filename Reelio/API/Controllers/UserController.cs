using BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Common.DTO;
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


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO model)
        {
            UserDTO user = new UserDTO
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
            userService.RegisterUser(user);
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }
    }
}
