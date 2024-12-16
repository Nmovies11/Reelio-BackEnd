using BLL.Helpers;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Authentication;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly JWTHelper jwtHelper;
        private readonly IConfiguration _configuration;



        public UserService(IUserRepository iuserRepository, IConfiguration configuration)
        {
            userRepository = iuserRepository;
            _configuration = configuration;
            jwtHelper = new JWTHelper(_configuration["JWTSECRET"]);
        }
        public async Task<string?> RegisterUser(UserDTO user)
        {

            user.Password = HashHelper.Encrypt(user.Password);
            Guid newUserId = Guid.NewGuid();

            UserDTO userDTO = new UserDTO
            {
                Id = newUserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            await userRepository.AddUser(userDTO);
            return jwtHelper.GenerateToken(new UserJWTDTO(user.Id, user.Username, user.Email));
        }


        public async Task<ResponseDTO> Authenticate(LoginDTO body)
        {
            if (body == null)
            {
                return new ResponseDTO
                {
                    Message = "Invalid request",
                };
            }

            UserDTO user = await userRepository.GetUserByEmail(body.Email);
            if (user == null || !HashHelper.Verify(body.Password, user.Password))
            {
                return new ResponseDTO
                {
                    Message = "Invalid credentials",
                };
            }

            UserJWTDTO userJWT = new UserJWTDTO(user.Id, user.Username, user.Email);

            return new ResponseDTO
            {
                Token = jwtHelper.GenerateToken(userJWT),
                Message = "Success"
            };
        }

        public UserJWTDTO? ValidateToken(string token)
        {
           return jwtHelper.ValidateToken(token);
        }

    }
}
