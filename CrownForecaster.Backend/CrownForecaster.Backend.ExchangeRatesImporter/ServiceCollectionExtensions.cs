using CrownForecaster.Backend.ExchangeRatesApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.ExchangeRatesImporter;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterExchangeRatesImporter(this ServiceCollection serviceCollection)
    {
        serviceCollection.RegisterExchangeRatesApiClient();

        serviceCollection.AddSingleton<IFxRateHistoricalDataFileWriter, FxRateHistoricalDataFileWriter>();
        serviceCollection.AddSingleton<IFxRateHistoricalDataConverter, FxRateHistoricalDataConverter>();

        string? exchangeRatesApiAccessToken = Environment.GetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY");
        serviceCollection.AddSingleton<IExchangeRatesImporter, ExchangeRatesImporter>(sp => new ExchangeRatesImporter(
            sp.GetRequiredService<IExchangeRatesApiClient>(),
            sp.GetRequiredService<IFxRateHistoricalDataFileWriter>(),
            exchangeRatesApiAccessToken));

        return serviceCollection;
    }
}
