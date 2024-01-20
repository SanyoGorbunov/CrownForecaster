using CrownForecaster.Shared.Domain.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Shared.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection RegisterDomainServices(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFxRateHistoricalDataConverter, FxRateHistoricalDataConverter>();

            return serviceCollection;
        }
    }
}
