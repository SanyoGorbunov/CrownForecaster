using CrownForecaster.Backend.ExchangeRatesApiClient;

namespace CrownForecaster.Backend.ExchangeRatesImporter;

internal class ExchangeRatesImporter : IExchangeRatesImporter
{
    private IExchangeRatesApiClient _exchangeRatesApiClient;
    private string _exchangeRatesApiAccessKey;

    public ExchangeRatesImporter(IExchangeRatesApiClient exchangeRatesApiClient, string? exchangeRatesApiAccessKey)
    {
        _exchangeRatesApiClient = exchangeRatesApiClient;
        _exchangeRatesApiAccessKey = exchangeRatesApiAccessKey ?? throw new ArgumentNullException(nameof(exchangeRatesApiAccessKey));
    }

    public Task ImportExchangeRates(DateOnly startDate, DateOnly endDate, string path)
    {
        throw new NotImplementedException();
    }
}