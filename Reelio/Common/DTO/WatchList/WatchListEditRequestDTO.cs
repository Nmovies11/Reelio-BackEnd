using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.WatchList
{
    public class WatchListEditRequestDTO
    {
        [Required]
        [RegularExpression("watched|plan to watch", ErrorMessage = "Status must be 'watched' or 'plan to watch'.")]
        public string Status { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
        public double? Rating { get; set; }

        [MaxLength(500, ErrorMessage = "Review cannot exceed 500 characters.")]
        public string? Review { get; set; }
    }
}
