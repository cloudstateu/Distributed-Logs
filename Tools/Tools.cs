using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common
{
    public static class Tools
    {
        private static readonly HttpClient _client;

        static Tools()
        {
            _client = new HttpClient();
        }

        public static Task<HttpResponseMessage> CallApi(string apiName, string method)
        {
            var req = new HttpRequestMessage();
            req.RequestUri = 
                new Uri($"http://{apiName}/api/{method}");

            return _client.SendAsync(req);
        }
    }
}
