 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace BLL.Interfaces.Services
{
    public interface IMovieService
    {
        public Task<List<MovieDTO>> GetRecentMovies();
        public Task<MovieDTODetails> GetMovieById(int id);

    }
}
