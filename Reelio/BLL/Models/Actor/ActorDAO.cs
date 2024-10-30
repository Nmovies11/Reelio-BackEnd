using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Actor
{
    public class ActorDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Movie.MovieDAO>? Movies { get; set; }
    }
}
