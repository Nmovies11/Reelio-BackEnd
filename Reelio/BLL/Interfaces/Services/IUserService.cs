using Common.DTO;
using Common.DTO.Authentication;
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

    }
}
