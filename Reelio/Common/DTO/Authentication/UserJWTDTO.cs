using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class UserJWTDTO
    {
        public UserJWTDTO(Guid id, string name, string email)
        {
            Id = id;

            Name = new Regex(@"[^A-Za-z0-9 ]").Replace(name, "");
            Email = email;
        }


        [JsonIgnore] public Guid Id { get; set; }

        public string Name { get; set;  }
        public string Email { get; set; }
    }
}
