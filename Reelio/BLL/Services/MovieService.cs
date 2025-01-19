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

        public async Task<List<MovieDTO>> GetRecentMovies()
        {
            return await movieAPIRepository.GetRecentMovies();
        }

        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            return await movieAPIRepository.GetMovieById(id);
        }

        public async Task<PaginatedList<MovieDTO>> GetMovies(int pageNumber, int pageSize, string searchQuery, string genre)
        {
            return await movieAPIRepository.GetMovies(pageNumber, pageSize, searchQuery, genre);
        }
    }
}
