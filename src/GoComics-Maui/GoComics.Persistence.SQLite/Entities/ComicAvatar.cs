using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Persistence.SQLite.Entities
{
    public class ComicAvatar
    {
        public int Id { get; set; }
        public int ComicId { get; set; }
        public byte[]? Blob { get; set; } 
        public string? HashBytes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
