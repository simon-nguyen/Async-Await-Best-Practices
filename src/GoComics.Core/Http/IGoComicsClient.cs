using System.Collections.Generic;
using System.Threading;

namespace GoComics.Core.Http;

public interface IGoComicsClient
{
    IAsyncEnumerable<T> GetItemsAsync<T>(IScrapeItems<T> strategy, CancellationToken cancellationToken = default);
}
