using Common.DTO.Season;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ShowDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BackdropUrl { get; set; }

        public ICollection<SeasonDTO>? Seasons { get; set; } = new List<SeasonDTO>();
    }
}
