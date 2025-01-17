using BLL.Interfaces.Repositories;
using Common.DTO;
using Microsoft.EntityFrameworkCore;

using Common.Entities;
using Common.DTO.WatchList;
using DAL.Entities.User;
using Common.DTO.Authentication;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddUser(UserDTO user)
        {
            var userEntity = new User
            {
                Id = Guid.NewGuid(), 
                Username = user.Username,
                Email = user.Email,
                Password = user.Password 
            };
            _context.Users.Add(userEntity);
                 _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var userEntity = await  _context.Users.FirstOrDefaultAsync(x => x.Email == email);


            // Return null if user not found
            if (userEntity == null)
            {
                return null;
            }

            // Map the User entity to UserDTO
            var userDto = new UserDTO
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
                Password = userEntity.Password
            };

            return userDto;
        }

        public async Task<UserDTODetails> GetUserById(Guid id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            // Return null if user not found
            if (userEntity == null)
            {
                return null;
            }

            // Map the User entity to UserDTO
            var userDto = new UserDTODetails
            {
                Username = userEntity.Username,
                Email = userEntity.Email,
            };

            return userDto;
        }


        public List<WatchListDTO> GetWatchlist(Guid userId)
        {
            List<Watchlist> watchListEntities = _context.Watchlists
                .Where(w => w.UserId == userId)
                .ToList();

            List<WatchListDTO> watchListDTOs = watchListEntities.Select(w => new WatchListDTO
            {
                Id = w.Id,
                ContentId = w.ContentId,
                ContentType = w.ContentType,
                Status = w.Status,
                Rating = w.Rating,
                Review = w.Review,
            }).ToList();

            return watchListDTOs;


        }


        public WatchListDTO AddToWatchlist(Guid userId, CreateWatchlistDTO dto)
        {
            // Convert DTO to Entity
            var watchlistEntity = new Watchlist
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ContentId = dto.ContentId,
                ContentType = dto.ContentType.ToLower(),
                Status = dto.Status,
                Rating = dto.Rating,
                Review = dto.Review,
                CreatedAt = DateTime.UtcNow
            };

            // Add the entity to the database
            _context.Watchlists.Add(watchlistEntity);
            _context.SaveChanges();

            // Convert the saved entity to a DTO and return it
            var watchlistDto = new WatchListDTO
            {
                Id = watchlistEntity.Id,
                ContentId = watchlistEntity.ContentId,
                ContentType = watchlistEntity.ContentType,
                Status = watchlistEntity.Status,
                Rating = watchlistEntity.Rating,
                Review = watchlistEntity.Review,
            };

            return watchlistDto;
        }

        public async Task<bool> RemoveFromWatchlistAsync(Guid userId, Guid watchlistItemId)
        {
            var watchlistItem = await _context.Watchlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.Id == watchlistItemId);

            if (watchlistItem == null)
            {
                return false; 
            }

            _context.Watchlists.Remove(watchlistItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }


}
