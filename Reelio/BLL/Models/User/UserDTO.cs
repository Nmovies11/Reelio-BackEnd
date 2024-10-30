using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.User
{
    [Table("users")]
    public class UserDTO
    {
        public int Id { get; set; }
        [Column("user_name")]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
