using BLL.Helpers;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.Entities;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository iuserRepository)
        {
            userRepository = iuserRepository;
        }
        public async Task RegisterUser(UserDTO user)
        {

            user.Password = EncryptionHelper.Encrypt(user.Password);
            Guid newUserId = Guid.NewGuid();

            User userDTO = new User
            {
                Id = newUserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            await userRepository.AddUser(userDTO);
        }




    }
}
