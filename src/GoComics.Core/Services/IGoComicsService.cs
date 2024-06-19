using GoComics.Core.Models;
using System.Collections.Generic;
using System.Threading;

namespace GoComics.Core.Services;

public interface IGoComicsService
{
    IAsyncEnumerable<ComicTileModel> GetAllComicsAsync(CancellationToken cancellationToken = default);
}
