using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace GoComics.Persistence.SQLite.Entities
{
    public class Comic
    {
        [PrimaryKey]
        [AutoIncrement]
        public int? Id { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? OptimisticLock { get; set; }
    }
}
