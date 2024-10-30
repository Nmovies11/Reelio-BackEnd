using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Show;

namespace BLL.Interfaces.Services
{
    public interface IShowService
    {
        public Task<List<ShowDAO>> GetRecentShows();
        public Task<ShowDAO> GetShowById(int id);
    }
}
