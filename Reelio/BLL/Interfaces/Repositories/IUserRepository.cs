using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace BLL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(User user);
    }
}
