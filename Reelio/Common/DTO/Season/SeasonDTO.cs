using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class SeasonDTO
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int ShowId { get; set; }
        public ICollection<EpisodeDTO>? Episodes { get; set; }
    }
}
