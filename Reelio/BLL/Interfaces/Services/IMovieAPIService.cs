using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IMovieAPIService
    {
        public Task<List<MovieDAO>> GetRecentMovie();
        public Task<MovieDAO> GetMovieById(int id);
    }
}
