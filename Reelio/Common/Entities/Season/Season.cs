using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Entities
{
    [NotMapped]
    public class Season
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("showId")]
        public int ShowId { get; set; }

        [JsonIgnore] 
        public Show Show { get; set; }

        [JsonPropertyName("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("season_name")]   
        public string SeasonName { get; set; }

        [JsonPropertyName("episode_count")]
        public int EpisodeCount { get; set; }

        [JsonPropertyName("releaseDate")]
        public DateOnly ReleaseDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("poster_url")]
        public string ImageUrl { get; set; }

        // Navigational property for episodes (should be serialized as a list of EpisodeDTOs)
        [JsonPropertyName("episodes")]
        public ICollection<Episode> Episodes { get; set; }



    }
}
