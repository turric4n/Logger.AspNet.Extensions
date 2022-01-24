using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Logger.AspNet.Extensions
{
    public class CorrelationService : ICorrelationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        internal const string CorrelationHeaderIdKey = "correlationId";

        internal const string CorrelationHeaderCallerNameKey = "correlationCallerName";

        internal const string CorrelationHeaderCallerMethodKey = "correlationCallerMethod";

        internal const string DefaultCallerName = "Undefined";

        public CorrelationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Correlation GetCurrentCorrelation()
        {
            var context = _httpContextAccessor.HttpContext;

            context.Request.Headers.TryGetValue(CorrelationHeaderIdKey, out var correlationId);

            context.Request.Headers.TryGetValue(CorrelationHeaderCallerNameKey, out var correlationCallerName);

            context.Request.Headers.TryGetValue(CorrelationHeaderCallerMethodKey, out var correlationCallerMethod);

            correlationId = string.IsNullOrEmpty(correlationId.ToString()) ? Guid.NewGuid().ToString() : correlationId;

            correlationCallerName = string.IsNullOrEmpty(correlationCallerName.ToString()) ? DefaultCallerName : correlationCallerName;

            correlationCallerMethod = string.IsNullOrEmpty(correlationCallerMethod.ToString()) ? DefaultCallerName : correlationCallerMethod;

            return new Correlation()
            {
                CorrelationCallerMethod = correlationCallerMethod, CorrelationCallerName = correlationCallerName,
                CorrelationId = correlationId
            };
        }
    }
}
