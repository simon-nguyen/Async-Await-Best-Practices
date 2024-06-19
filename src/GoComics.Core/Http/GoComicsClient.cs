using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoComics.Core.Http;

public class GoComicsClient : IGoComicsClient
{
    //private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    //public GoComicsClient(IHttpClientFactory httpClientFactory)
    //{
    //    _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    //}
    
    public GoComicsClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async IAsyncEnumerable<T> GetItemsAsync<T>(IScrapeItems<T> scrape, CancellationToken cancellationToken = default)
    {
        //var client = _httpClientFactory.GetClient("Comics");

        var request = new HttpRequestMessage(HttpMethod.Get, scrape.Name);
        Stream responseStream;
        try
        {
            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw;
        }

        var html = new HtmlDocument();
        html.Load(responseStream);

        foreach (T item in scrape.Scrape(html))
        {
            yield return item;
        }
    }
}
