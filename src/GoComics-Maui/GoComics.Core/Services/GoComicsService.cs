using GoComics.Core.Models;
using HtmlAgilityPack;
using System.Runtime.CompilerServices;

namespace GoComics.Core.Services;

// All the code in this file is included in all platforms.
public class GoComicsService(HttpClient _httpClient)
{
    private const int MaxBatchSize = 10;

    public IAsyncEnumerable<ComicTileModel> GetAllComicsAsync(CancellationToken cancellationToken = default)
    {
        var strategy = new ComicTilesScrapingStrategy("https://www.gocomics.com/comics/a-to-z/");

        return GetItemsAsync(strategy, cancellationToken);
    }

    private async IAsyncEnumerable<T> GetItemsAsync<T>(IScrapeItems<T> scrapingStrategy, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, scrapingStrategy.Name);
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            throw;
        }

        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var html = new HtmlDocument();
        html.Load(responseStream);

        var batch = new List<T>();

        foreach (T item in scrapingStrategy.Scrape(html))
        {
            //await item.DownloadAvatarAsync().ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested) yield break;

            await Task.Delay(150, cancellationToken);
            yield return item;
        }
    }
}
