using System;
using Common.DTO;
namespace BLL.Interfaces.Repositories
{
    public interface IShowRepository
    {
        public Task<List<ShowDTO>> GetRecentShows();
        public  Task<ShowDTO> GetShowById(int id);
    }
}
