using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;


namespace BLL.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        public Task<List<MovieDTO>> GetRecentMovies();
        public Task<MovieDTODetails> GetMovieById(int id);

    }
}
