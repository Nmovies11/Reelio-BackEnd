using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.API.Entities;


namespace DAL.API.Entities
{
    [NotMapped]
    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("releaseDate")]
        public DateOnly ReleaseDate { get; set; }
        [JsonPropertyName("director")]
        public string Director { get; set; }
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("backdropUrl")]
        public string BackdropUrl { get; set; }

        [JsonPropertyName("actors")]
        public ICollection<Actor>? Actors { get; set; }

    }
}
