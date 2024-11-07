using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;


namespace BLL.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        public Task<List<Movie>> GetRecentMovie();
        public Task<Movie> GetMovieById(int id);

    }
}
