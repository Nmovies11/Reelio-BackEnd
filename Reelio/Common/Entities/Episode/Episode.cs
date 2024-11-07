using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Episode
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("season_id")]
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        [Column("episode_number")]
        public int EpisodeNumber { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }
        [Column("director")]
        public string Director { get; set; }
    }
}
