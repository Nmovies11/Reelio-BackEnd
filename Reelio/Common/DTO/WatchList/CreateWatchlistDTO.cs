using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.WatchList
{
    public class CreateWatchlistDTO
    {
        public string ContentId { get; set; } 
        public string ContentType { get; set; }
        public string Status { get; set; }
        public double? Rating { get; set; }
        public string? Review { get; set; }

    }
}
