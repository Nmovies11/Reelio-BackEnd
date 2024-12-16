using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.DTO;
namespace BLL.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieAPIRepository;

        public MovieService(IMovieRepository imovieAPIRepository)
        {
            movieAPIRepository = imovieAPIRepository;
        }

        //get recent movie
        public async Task<List<MovieDTO>> GetRecentMovies()
        {
            return await movieAPIRepository.GetRecentMovies();
        }


        //get movie by ID
        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            // Service layer only interacts with DTOs
            return await movieAPIRepository.GetMovieById(id);
        }
    }
}
