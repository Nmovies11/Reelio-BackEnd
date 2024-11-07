using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Episode
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("season_id")]
        public int SeasonId { get; set; }

        [JsonIgnore]
        public Season Season { get; set; }

        [JsonPropertyName("episode_number")]
        public int EpisodeNumber { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("release_date")]
        public DateOnly ReleaseDate { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }
    }
}
