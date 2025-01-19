using BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Common.DTO;
using Azure;
using BLL.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Common.Entities;
using DAL.Entities.User;
using Common.DTO.WatchList;
using Common.DTO.Authentication;

namespace API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        [Route("/account")]
        public UserJWTDTO? GetAccount()
        {
            var user = HttpContext.Items["User"] as UserJWTDTO;
            if (user == null)
            {
                Console.WriteLine("User not found in HttpContext.Items");
            }
            Console.WriteLine($"User data: {user.Id}");

            return user;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTODetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  Task<UserDTODetails> GetUserById(Guid id)
        {
            var user = userService.GetUserById(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return user;
        }

 


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

            return CreatedAtAction(nameof(GetAccount), new { }, new { message = "User registered successfully", token = token });
        }

        [HttpGet("{id}/watchlist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ShowDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<WatchListDTO>> GetWatchlist(Guid id)
        {
            
            var watchlist = userService.GetWatchlist(id);

            return watchlist;
        }

        [HttpPost("{id}/watchlist")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToWatchlist(Guid id, [FromBody] CreateWatchlistDTO watchlistItem)
        {
            if (watchlistItem == null)
            {
                return BadRequest("Watchlist item data is required.");
            }

            try
            {
                WatchListDTO response =  userService.AddToWatchlist(id, watchlistItem);

                if (response == null)
                {
                    return NotFound("User not found or unable to add item to watchlist.");
                }

                // Return the created watchlist item along with its location
                return CreatedAtAction(nameof(GetWatchlist), new { id = response.Id }, response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


        [HttpDelete("{userId}/watchlist/{watchlistItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]  
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> RemoveFromWatchlist(Guid userId, string watchlistItemId, [FromQuery] string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                return BadRequest("Content type is required.");
            }

            var result = await userService.RemoveFromWatchlist(userId, watchlistItemId, contentType);

            if (!result)
            {
                return NotFound("Watchlist item not found.");
            }

            return NoContent();
        }

    }
}
