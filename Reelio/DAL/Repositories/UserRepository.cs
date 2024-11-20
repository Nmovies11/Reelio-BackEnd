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
        public Task AddUser(User user)
        {
                 _context.Users.Add(user);
                 _context.SaveChanges();
            return Task.CompletedTask;
        }

    }
}
