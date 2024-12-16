using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace BLL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(UserDTO user);
        public Task<UserDTO> GetUserByEmail(string email);
    }
}
