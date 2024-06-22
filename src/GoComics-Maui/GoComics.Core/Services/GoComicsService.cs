using GoComics.Core.Models;
using HtmlAgilityPack;
using System.Runtime.CompilerServices;

namespace GoComics.Core.Services;

// All the code in this file is included in all platforms.
public class GoComicsService(IHttpClientFactory _httpClientFactory)
{
    public IAsyncEnumerable<ComicTileModel> GetAllComicsAsync(CancellationToken cancellationToken = default)
    {
        string key = "a-to-z";
        var strategy = new ComicTilesScrapingStrategy(key);

        return GetItemsAsync(strategy, cancellationToken);
    }

    private async IAsyncEnumerable<T> GetItemsAsync<T>(IScrapeItems<T> scrapingStrategy, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient("Comics");
        var request = new HttpRequestMessage(HttpMethod.Get, scrapingStrategy.Name);
        HttpResponseMessage response;
        try
        {

            response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            throw;
        }

        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var html = new HtmlDocument();
        html.Load(responseStream);

        foreach (T item in scrapingStrategy.Scrape(html))
        {
            if (cancellationToken.IsCancellationRequested) yield break;
            yield return item;
        }
    }
}
