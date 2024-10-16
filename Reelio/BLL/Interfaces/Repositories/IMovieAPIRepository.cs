using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Repositories
{
    public interface IMovieAPIRepository
    {
        public Task<List<MovieDTO>> GetRecentMovie();
        public Task<MovieDTO> GetMovieById(int id);

    }
}
