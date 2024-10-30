using BLL.Interfaces.Services;
using BLL.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult Register([FromBody] UserDAO model)
        {
            UserDAO user = new UserDAO
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
