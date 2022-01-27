using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Logger.Extensions
{
    public class CorrelationService : ICorrelationService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        internal const string CorrelationHeaderIdKey = "correlationId";

        internal const string CorrelationHeaderCallerNameKey = "correlationCaller";

        internal const string CorrelationHeaderCallerMethodKey = "correlationMethod";

        internal const string DefaultCallerName = "Undefined";

        public CorrelationService(IHttpContextAccessor httpContextAccessor = null)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static void ReplaceHeader(HttpContext context, string name, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

                context?.Request?.Headers.TryGetValue(name, out var current);

            if (!string.IsNullOrEmpty(current))
            {
                context?.Request?.Headers.Remove(name);
            }

            context?.Request?.Headers.TryAdd(name, value);
        }

        public Correlation GetCurrentCorrelation()
        {
            var context = _httpContextAccessor?.HttpContext;

            context?.Request?.Headers.TryGetValue(CorrelationHeaderIdKey, out var correlationId);

            context?.Request?.Headers.TryGetValue(CorrelationHeaderCallerNameKey, out var correlationCallerName);

            context?.Request?.Headers.TryGetValue(CorrelationHeaderCallerMethodKey, out var correlationCallerMethod);

            correlationId = string.IsNullOrEmpty(correlationId.ToString()) ? (StringValues)Guid.NewGuid().ToString() : correlationId;

            correlationCallerName = string.IsNullOrEmpty(correlationCallerName.ToString()) ? (StringValues)DefaultCallerName : correlationCallerName;

            correlationCallerMethod = string.IsNullOrEmpty(correlationCallerMethod.ToString()) ? (StringValues)DefaultCallerName : correlationCallerMethod;

            return new Correlation()
            {
                CorrelationCallerMethod = correlationCallerMethod, CorrelationCallerName = correlationCallerName,
                CorrelationId = correlationId
            };
        }

        public void SetCurrentCorrelation(Correlation correlation)
        {
            var context = _httpContextAccessor?.HttpContext;

            if (context == null) return; 

            ReplaceHeader(context, CorrelationHeaderCallerMethodKey, correlation?.CorrelationCallerMethod);

            ReplaceHeader(context, CorrelationHeaderCallerNameKey, correlation?.CorrelationCallerName);

            ReplaceHeader(context, CorrelationHeaderIdKey, correlation?.CorrelationId);
        }
    }
}
