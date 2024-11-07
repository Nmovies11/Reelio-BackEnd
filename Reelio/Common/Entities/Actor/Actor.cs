using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Actor
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("birthdate")]
        public DateTime BirthDate { get; set; }
        [Column("biography")]
        public string Bio { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
