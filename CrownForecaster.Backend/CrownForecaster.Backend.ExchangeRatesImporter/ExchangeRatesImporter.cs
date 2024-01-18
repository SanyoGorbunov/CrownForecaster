using CrownForecaster.Backend.ExchangeRatesApiClient;

namespace CrownForecaster.Backend.ExchangeRatesImporter;

internal class ExchangeRatesImporter : IExchangeRatesImporter
{
    private IExchangeRatesApiClient _exchangeRatesApiClient;
    private IFxRateRepository _fxRateRepository;
    private string _exchangeRatesApiAccessKey;

    public ExchangeRatesImporter(IExchangeRatesApiClient exchangeRatesApiClient, IFxRateRepository fxRateRepository, string? exchangeRatesApiAccessKey)
    {
        _exchangeRatesApiClient = exchangeRatesApiClient;
        _fxRateRepository = fxRateRepository;
        _exchangeRatesApiAccessKey = exchangeRatesApiAccessKey ?? throw new ArgumentNullException(nameof(exchangeRatesApiAccessKey));
    }

    public async Task ImportExchangeRates(DateOnly startDate, DateOnly endDate, string path)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException($"StartDate {startDate.ToShortDateString()} is later than EndDate {endDate.ToShortDateString()}");
        }

        DateOnly historicalDate = startDate;
        var fxRates = new List<FxRate>();
        while (historicalDate <= endDate)
        {
            var fxRate = await _exchangeRatesApiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, historicalDate, _exchangeRatesApiAccessKey);
            fxRates.Add(new FxRate(historicalDate, fxRate));

            historicalDate = historicalDate.AddDays(1);
        }

        await _fxRateRepository.SaveFxRates(fxRates);
    }
}