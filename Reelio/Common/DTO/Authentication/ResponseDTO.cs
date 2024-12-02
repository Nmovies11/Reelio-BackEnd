using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Authentication
{
    public class ResponseDTO
    {
        public string Token { get; set; }
        public UserJWTDTO User { get; set; }
        public string Message { get; set; }
    }
}
