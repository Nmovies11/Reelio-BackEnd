using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Show;

namespace BLL.Interfaces.Repositories
{
    public interface IShowRepository
    {
        public Task<List<ShowDTO>> GetRecentShows();
        public Task<ShowDTO> GetShowById(int id);
    }
}
