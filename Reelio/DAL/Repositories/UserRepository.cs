using BLL.Interfaces.Repositories;
using Common.DTO;
using Microsoft.EntityFrameworkCore;

using Common.Entities;

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
                Username = userEntity.Username,
                Email = userEntity.Email,
            };

            return userDto;
        }
    }

   
}
