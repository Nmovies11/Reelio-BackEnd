using BLL.Interfaces.Repositories;
using BLL.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                 _context.Users.Add(user);
                 _context.SaveChanges();
            return Task.CompletedTask;
        }

    }
}
