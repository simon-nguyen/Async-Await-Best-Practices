using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Http
{
    public interface IGoComicsClient
    {
        IAsyncEnumerable<T> GetItemsAsync<T>(IScrapeItems<T> strategy, CancellationToken cancellationToken = default);
    }
}
