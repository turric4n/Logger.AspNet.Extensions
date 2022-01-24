using Logger.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorrelationExtensions
    {
        public static IServiceCollection AddCorrelationService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICorrelationService, CorrelationService>();

            return serviceCollection;
        }
    }
}
