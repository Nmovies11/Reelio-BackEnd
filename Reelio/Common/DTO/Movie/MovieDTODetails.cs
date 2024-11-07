using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Common.DTO;

namespace Common.DTO
{
    public class MovieDTODetails
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public string BackdropUrl { get; set; }
        public int Runtime { get; set; }
        public ICollection<ActorDTO>? Actors { get; set; } = new List<ActorDTO>();

    }
}
