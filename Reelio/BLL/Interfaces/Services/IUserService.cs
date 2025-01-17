using Common.DTO;
using Common.DTO.Authentication;
using Common.DTO.WatchList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        public Task<string?> RegisterUser(UserDTO user);
        public Task<ResponseDTO> Authenticate(LoginDTO body);
        public UserJWTDTO? ValidateToken(string token);
        public Task<UserDTODetails> GetUserById(Guid id);
        public List<WatchListDTO> GetWatchlist(Guid userId);
        public WatchListDTO AddToWatchlist(Guid userId, CreateWatchlistDTO dto);
        public Task<bool> RemoveFromWatchlist(Guid userId, Guid watchlistItemId);

    }
}
