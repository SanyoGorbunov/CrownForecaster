namespace CrownForecaster.Backend.ExchangeRatesImporter;

internal class ExchangeRatesImporter : IExchangeRatesImporter
{
    private string _exchangeRatesApiAccessKey;

    public ExchangeRatesImporter(string? exchangeRatesApiAccessKey)
    {
        _exchangeRatesApiAccessKey = exchangeRatesApiAccessKey ?? throw new ArgumentNullException(nameof(exchangeRatesApiAccessKey));
    }

    public Task ImportExchangeRates(DateOnly startDate, DateOnly endDate, string path)
    {
        throw new NotImplementedException();
    }
}