using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Actor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("birthdate")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("biography")]
        public string Bio { get; set; }
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
