using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Persistence.SQLite.Entities
{
public class ComicImage
    {
        public int Id { get; set; }
        public int ComicId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? OptimisticLock { get; set; }
    }
}
