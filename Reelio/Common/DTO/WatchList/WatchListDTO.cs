using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.WatchList
{
    public class WatchListDTO
    {
        public Guid Id { get; set; }  
        public string ContentId { get; set; }  
        public string ContentType { get; set; } 
        public string Status { get; set; }  
        public double? Rating { get; set; }  
        public string? Review { get; set; }  

    }
}
