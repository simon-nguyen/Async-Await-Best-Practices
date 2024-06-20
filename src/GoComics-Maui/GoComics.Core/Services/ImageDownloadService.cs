using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Services;

public class ImageDownloadService(HttpClient _httpClient)
{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <remarks>Uses async/await here so we can capture the exception (if occurs).</remarks>
    public async ValueTask<Stream?> DownloadImageStreamAsync(string url,CancellationToken cancellationToken=default)
    {
				try
				{
						return await _httpClient.GetStreamAsync(url, cancellationToken).ConfigureAwait(false);
				}
				catch (Exception)
				{
						throw;
				}
    }
}
