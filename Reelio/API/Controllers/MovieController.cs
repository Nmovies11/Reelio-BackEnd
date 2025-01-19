using BLL.Interfaces.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("movies")]
    public class MovieController : Controller
    {

        private readonly IMovieService movieService;
        public MovieController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTODetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieById(int id)
        {
            MovieDTODetails movie = await movieService.GetMovieById(id);
            if(movie  == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MovieDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginatedList<MovieDTO>> GetMovies(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchQuery = null,
            [FromQuery] string? genre = null)
            {
            return await movieService.GetMovies(pageNumber, pageSize, searchQuery, genre);
        }
        
    }
}
