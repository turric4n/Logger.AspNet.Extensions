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

        internal const string CorrelationHeaderCallerNameKey = "correlationCallerName";

        internal const string CorrelationHeaderCallerMethodKey = "correlationCallerMethod";

        internal const string DefaultCallerName = "Undefined";

        public CorrelationService(IHttpContextAccessor httpContextAccessor = null)
        {
            _httpContextAccessor = httpContextAccessor;
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

            context?.Request?.Headers.TryAdd(CorrelationHeaderIdKey, correlation.CorrelationId);

            context?.Request?.Headers.TryAdd(CorrelationHeaderCallerMethodKey, correlation.CorrelationCallerMethod);

            context?.Request?.Headers.TryAdd(CorrelationHeaderCallerNameKey, correlation.CorrelationCallerName);
        }
    }
}
