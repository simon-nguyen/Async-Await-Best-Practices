using HtmlAgilityPack;

namespace GoComics.Core.Services;

public interface IScrapeItems<T>
{
    string Name { get; }

    IEnumerable<T> Scrape(HtmlDocument html);
}