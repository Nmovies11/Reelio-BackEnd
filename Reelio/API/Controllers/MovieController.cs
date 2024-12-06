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

        [HttpGet("recentmovies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<MovieDTO>> GetRecentMovie()
        {
            return await movieService.GetRecentMovie();
        }

        [HttpGet("TestingAzureContainer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<String> TestInfo()
        {
            return "Yo";
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTODetails))]
        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            return await movieService.GetMovieById(id);
        }

        
    }
}
