using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Runtime { get; set; }
        public int ShowId { get; set; }
        public int EpisodeNumber { get; set; }
    }
}
