using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
namespace BLL.Interfaces.Services
{
    public interface IShowService
    {
        public Task<List<ShowDTO>> GetRecentShows();
        public Task<ShowDTO> GetShowById(int id);
    }
}
