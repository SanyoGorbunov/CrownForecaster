using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ExchangeRatesApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Tools.ExchangeRatesImporter;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterExchangeRatesImporter(this ServiceCollection serviceCollection)
    {
        serviceCollection.RegisterExchangeRatesApiClient();
        serviceCollection.RegisterDomainServices();

        serviceCollection.AddSingleton<IFxRateHistoricalDataFileWriter, FxRateHistoricalDataFileWriter>();

        string? exchangeRatesApiAccessToken = Environment.GetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY");
        serviceCollection.AddSingleton<IExchangeRatesImporter, ExchangeRatesImporter>(sp => new ExchangeRatesImporter(
            sp.GetRequiredService<IExchangeRatesApiClient>(),
            sp.GetRequiredService<IFxRateHistoricalDataFileWriter>(),
            exchangeRatesApiAccessToken));

        return serviceCollection;
    }
}
