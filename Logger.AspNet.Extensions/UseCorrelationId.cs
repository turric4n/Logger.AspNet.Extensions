using Logger.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions
{
    public static class UseCorrelationIdExtensions
    {
        public static IApplicationBuilder UseLoggerCorrelationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
}
