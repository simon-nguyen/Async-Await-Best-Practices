
using HtmlAgilityPack;

namespace GoComics.Core
{
    public interface IScrapeItems<T>
    {
        string Name { get;  }

        IEnumerable<T> Scrape(HtmlDocument html);
    }

}
