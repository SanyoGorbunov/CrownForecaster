using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.ExchangeRatesImporter;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterExchangeRatesImporter(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IExchangeRatesImporter, ExchangeRatesImporter>();

        return serviceCollection;
    }
}
