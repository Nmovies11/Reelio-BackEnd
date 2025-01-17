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
        public UserJWTDTO(Guid id)
        {
            Id = id;

        }


         public Guid Id { get; set; }

    }
}
