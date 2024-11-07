using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Season
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("show_id")]
        public int ShowId;
        public Show Show { get; set; }
        [Column("season_number")]
        public int SeasonNumber { get; set; }
        [Column("season_name")]
        public string SeasonName { get; set; }
        [Column("episode_count")]
        public int EpisodeCount { get; set; }
        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("poster_url")]
        public string ImageUrl { get; set; }

        public required ICollection<Episode> Episodes { get; set; }


    }
}
