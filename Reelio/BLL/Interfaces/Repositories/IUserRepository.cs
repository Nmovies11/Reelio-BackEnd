using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Authentication;
using Common.DTO.WatchList;

namespace BLL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(UserDTO user);
        public Task<UserDTO> GetUserByEmail(string email);
        public List<WatchListDTO> GetWatchlist(Guid userId);
        public Task<UserDTODetails> GetUserById(Guid id);
        public WatchListDTO AddToWatchlist(Guid userId, CreateWatchlistDTO dto);
        public Task<bool> RemoveFromWatchlistAsync(Guid userId, string watchlistItemId, string contentType);

    }
}
