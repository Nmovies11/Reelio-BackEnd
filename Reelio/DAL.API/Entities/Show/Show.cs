using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DAL.API.Entities;

namespace DAL.API.Entities
{
    [NotMapped]
    public class Show
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("releaseDate")]
        public DateOnly ReleaseDate { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("backdropUrl")]
        public string BackdropUrl { get; set; }

        [JsonPropertyName("seasons")]
        public ICollection<Season>? Seasons { get; set; }
    }
}
