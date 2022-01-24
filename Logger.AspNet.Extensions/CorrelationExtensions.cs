using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logger.AspNet.Extensions;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorrelationExtensions
    {
        public static IServiceCollection AddCorrelationService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICorrelationService, CorrelationService>();
            serviceCollection.AddHttpContextAccessor();

            return serviceCollection;
        }
    }
}
