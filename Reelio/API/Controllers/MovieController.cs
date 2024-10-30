using BLL.Interfaces.Services;
using BLL.Models.Movie;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {

        private readonly IMovieService movieService;
        public MovieController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        [HttpGet("recentmovies")]
        public async Task<List<MovieDAO>> GetRecentMovie()
        {
            return await movieService.GetRecentMovie();
        }

        [HttpGet("{id}")]
        public async Task<MovieDAO> GetMovieById(int id)
        {
            return await movieService.GetMovieById(id);
        }


    }
}
