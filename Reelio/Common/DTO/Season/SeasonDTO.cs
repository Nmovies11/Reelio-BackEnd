using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Season
{
    public class SeasonDTO
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ShowId { get; set; }
        public ICollection<EpisodeDTO>? Episodes { get; set; }
    }
}
