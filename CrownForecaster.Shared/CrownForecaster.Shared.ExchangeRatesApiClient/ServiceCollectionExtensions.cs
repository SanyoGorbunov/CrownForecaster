using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Shared.ExchangeRatesApiClient;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterExchangeRatesApiClient(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IExchangeRatesApiClient, ExchangeRatesApiClient>();
        serviceCollection.AddHttpClient();

        return serviceCollection;
    }
}
