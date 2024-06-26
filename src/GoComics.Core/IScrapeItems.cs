using HtmlAgilityPack;
using System.Collections.Generic;

namespace GoComics.Core;

public interface IScrapeItems<T>
{
    string Name { get; }

    IEnumerable<T> Scrape(HtmlDocument html);
}
