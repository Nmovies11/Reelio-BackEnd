using System;
using Common.DTO;
namespace BLL.Interfaces.Repositories
{
    public interface IShowRepository
    {
        public Task<List<ShowDTO>> GetRecentShows();
        public  Task<ShowDTO> GetShowById(int id);
        public Task<PaginatedList<ShowDTO>> GetShows(int pageNumber, int pageSize, string? searchQuery, string? genre);

    }
}
