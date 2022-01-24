using System;
using Logger.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions
{
    public static class UseKestrelLogging
    {
        public static IApplicationBuilder UseKestrelLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<KestrelLoggingMiddleware>();
        }
    }
}
