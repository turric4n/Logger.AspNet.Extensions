using Logger.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions
{
    public static class UseCorrelationIdExtensions
    {
        public static IApplicationBuilder UseLoggerCorrelationIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
}
