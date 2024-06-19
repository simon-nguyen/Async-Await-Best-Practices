using GoComics.Core.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace GoComics.Core.Services;

public class GoComicsService : IGoComicsService
{
    //private readonly IGoComicsClient _client;
    private readonly HttpClient _httpClient;

    //public GoComicsService(IGoComicsClient client)
    //{
    //    _client = client ?? throw new ArgumentNullException(nameof(client));
    //}

    public GoComicsService(HttpClient httpClient)
    {
        //_client = client ?? throw new ArgumentNullException(nameof(client));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public IAsyncEnumerable<ComicTileModel> GetAllComicsAsync(CancellationToken cancellationToken = default)
    {
        var strategy = new ComicTilesScrapingStrategy("https://www.gocomics.com/comics/a-to-z/");
        
        return GetItemsAsync(strategy, cancellationToken);
    }

    public async IAsyncEnumerable<T> GetItemsAsync<T>(IScrapeItems<T> scrape, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        //var client = _httpClientFactory.GetClient("Comics");

        cancellationToken.ThrowIfCancellationRequested();

        var request = new HttpRequestMessage(HttpMethod.Get, scrape.Name);
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

        foreach (T item in scrape.Scrape(html))
        {
            //await item.DownloadAvatarAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(100);
            yield return item;
        }
    }
}
