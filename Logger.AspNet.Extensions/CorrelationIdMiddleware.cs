using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logger.AspNet.Extensions
{
    internal class CorrelationMiddleware
    {
        internal const string CorrelationHeaderIdKey = "correlationId";

        internal const string CorrelationHeaderCallerNameKey = "correlationCaller";

        internal const string CorrelationHeaderCallerMethodKey = "correlationMethod";

        internal const string DefaultCallerName = "Undefined";

        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public CorrelationMiddleware(
            RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            _loggerFactory = loggerFactory;
        }

        private void AddHeaderIfNotPresent(HttpContext context, string headerName, string currentHeaderValue, string newHeaderValue)
        {
            if (string.IsNullOrEmpty(currentHeaderValue))
            {
                context.Request.Headers.Add(headerName, newHeaderValue);
            }
        }

        public async Task Invoke(HttpContext context)
        {
            var logger = _loggerFactory.CreateLogger<CorrelationMiddleware>();

            context.Request.Headers.TryGetValue(CorrelationHeaderIdKey, out var correlationId);

            context.Request.Headers.TryGetValue(CorrelationHeaderCallerNameKey, out var correlationCallerName);

            context.Request.Headers.TryGetValue(CorrelationHeaderCallerMethodKey, out var correlationCallerMethod);

            StringValues newCorrelationId = string.IsNullOrEmpty(correlationId.ToString()) ? Guid.NewGuid().ToString() : correlationId;

            StringValues newCorrelationCallerName = string.IsNullOrEmpty(correlationCallerName.ToString()) ? DefaultCallerName : correlationCallerName;

            StringValues newCorrelationCallerMethod = string.IsNullOrEmpty(correlationCallerMethod.ToString()) ? DefaultCallerName : correlationCallerMethod;

            AddHeaderIfNotPresent(context, CorrelationHeaderIdKey, correlationId, newCorrelationId);

            AddHeaderIfNotPresent(context, CorrelationHeaderCallerNameKey, correlationCallerName, newCorrelationCallerName);

            AddHeaderIfNotPresent(context, CorrelationHeaderCallerMethodKey, correlationCallerMethod, newCorrelationCallerMethod);

            using var scope = (logger.BeginScope(
                new Dictionary<string, object>()
                {
                    {"correlationId", newCorrelationId},
                    {"correlationCaller", newCorrelationCallerName},
                    {"correlationMethod", newCorrelationCallerMethod},
                }));

            await this._next.Invoke(context);
        }
    }
}
