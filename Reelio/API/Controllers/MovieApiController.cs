using BLL.Interfaces.Services;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieApiController : Controller
    {

        private readonly IMovieAPIService movieService;
        public MovieApiController(IMovieAPIService _movieService)
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
