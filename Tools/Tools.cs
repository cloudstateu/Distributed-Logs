using OpenTracing;
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

        public static Task<HttpResponseMessage> CallApi(string apiName, string method, ITracer tracer)
        {
            var req = new HttpRequestMessage();
            req.FillBaggage(tracer);

            req.RequestUri =
                new Uri($"http://{apiName}/api/{method}");

            return _client.SendAsync(req);
        }

        private static void FillBaggage(this HttpRequestMessage requestMessage, ITracer tracer)
        {
            var correlationId = tracer?.ActiveSpan?.GetBaggageItem("correlation-id");

            if (!string.IsNullOrEmpty(correlationId))
            {
                requestMessage.Headers.Add("correlation-id", correlationId);
            }
        }
    }
}
