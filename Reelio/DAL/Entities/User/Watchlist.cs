using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.User
{
    public class Watchlist
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("contentid")]
        public string ContentId { get; set; }

        [Column("contenttype")]
        public string ContentType { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("rating")]
        public double? Rating { get; set; }

        [Column("review")]
        public string? Review { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
