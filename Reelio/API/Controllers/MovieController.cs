using BLL.Interfaces.Services;
using Common.DTO;
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
        public async Task<List<MovieDTO>> GetRecentMovie()
        {
            return await movieService.GetRecentMovie();
        }

        [HttpGet("{id}")]
        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            return await movieService.GetMovieById(id);
        }


    }
}
