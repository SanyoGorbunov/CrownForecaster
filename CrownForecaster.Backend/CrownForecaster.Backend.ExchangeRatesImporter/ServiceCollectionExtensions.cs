using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.ExchangeRatesImporter;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterExchangeRatesImporter(this ServiceCollection serviceCollection)
    {
        string? exchangeRatesApiAccessToken = Environment.GetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY");
        serviceCollection.AddSingleton<IExchangeRatesImporter, ExchangeRatesImporter>(_ => new ExchangeRatesImporter(exchangeRatesApiAccessToken));

        return serviceCollection;
    }
}
