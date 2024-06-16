using GoComics.Core.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core
{
    public class ComicTilesScrapingStrategy : IScrapeItems<ComicTileModel>
    {
        public ComicTilesScrapingStrategy(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public IEnumerable<ComicTileModel> Scrape(HtmlDocument html)
        {
            var containerNode = html.DocumentNode.SelectSingleNode("//*[contains(concat(' ', normalize-space(@class), ' '), 'gc-container')]");
            if (containerNode is null)
                return Enumerable.Empty<ComicTileModel>();

            var rows = containerNode.SelectNodes("//*[contains(concat(' ', normalize-space(@class), ' '), 'row')]");
            return ScrapeItems(rows.Descendants("a"), row =>
            {
                var titleNode = row.Descendants("h4").FirstOrDefault();
                var byAuthorNode = row.Descendants("span").FirstOrDefault();
                var imageUrlNode = row.Descendants("img").FirstOrDefault();

                var comic = new ComicTileModel(
                    titleNode is null ? string.Empty : titleNode.InnerText,
                    byAuthorNode is null ? string.Empty : byAuthorNode.InnerText,
                    string.Empty,
                    string.Empty);

                return comic;
            });
        }

        public static IEnumerable<T> ScrapeItems<T>(IEnumerable<HtmlNode> nodes, Func<HtmlNode, T> map)
        {
            foreach (var node in nodes)
            {
                yield return map(node);
            }
        }
    }
}
