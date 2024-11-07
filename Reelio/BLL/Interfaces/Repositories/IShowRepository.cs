using System;
using Common.Entities;
namespace BLL.Interfaces.Repositories
{
    public interface IShowRepository
    {
        public Task<List<Show>> GetRecentShows();
        public Task<Show> GetShowById(int id);
    }
}
