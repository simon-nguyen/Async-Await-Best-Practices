using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Http
{
    public sealed class NamedHttpClientFactory : IHttpClientFactory
    {
        private readonly Dictionary<string, HttpClient> _clients = new();

        public HttpClient GetClient(string name)
        {
            if (_clients.ContainsKey(name))
                return _clients[name];

            HttpClient client = new();
            _clients[name] = client;

            return client;
        }
    }
}
