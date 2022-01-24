using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Logger.Extensions;

namespace Logger.AspNet.Extensions
{
    public static class CorrelationHttpClientExtensions
    {
        public static HttpClient UseCorrelationHeaders(this HttpClient client, string correlationId, string correlationCaller,
            string correlationMethod)
        {
            client.DefaultRequestHeaders.Remove("correlationId");
            client.DefaultRequestHeaders.Remove("correlationCaller");
            client.DefaultRequestHeaders.Remove("correlationMethod");

            client.DefaultRequestHeaders.Add("correlationId", correlationId);
            client.DefaultRequestHeaders.Add("correlationCaller", correlationCaller);
            client.DefaultRequestHeaders.Add("correlationMethod", correlationMethod);

            return client;
        }

        public static HttpClient UseCorrelationHeaders(this HttpClient client, Correlation correlation)
        {
            client.DefaultRequestHeaders.Remove("correlationId");
            client.DefaultRequestHeaders.Remove("correlationCaller");
            client.DefaultRequestHeaders.Remove("correlationMethod");

            client.DefaultRequestHeaders.Add("correlationId", correlation.CorrelationId);
            client.DefaultRequestHeaders.Add("correlationCaller", correlation.CorrelationCallerName);
            client.DefaultRequestHeaders.Add("correlationMethod", correlation.CorrelationCallerMethod);

            return client;
        }
    }
}
