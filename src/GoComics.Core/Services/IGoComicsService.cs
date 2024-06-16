using GoComics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Services
{
    public interface IGoComicsService
    {
        IAsyncEnumerable<ComicTileModel> GetAllComicsAsync(CancellationToken cancellationToken = default);
    }
}
