using CrownForecaster.Shared.ExchangeRatesApiClient;

namespace CrownForecaster.Tools.ExchangeRatesImporter;

internal class ExchangeRatesImporter : IExchangeRatesImporter
{
    private IExchangeRatesApiClient _exchangeRatesApiClient;
    private IFxRateHistoricalDataFileWriter _fxRateRepository;
    private string _exchangeRatesApiAccessKey;

    public ExchangeRatesImporter(IExchangeRatesApiClient exchangeRatesApiClient, IFxRateHistoricalDataFileWriter fxRateRepository, string? exchangeRatesApiAccessKey)
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

        var historicalData = FxRateHistoricalData.CreateFromFxRates(fxRates);

        await _fxRateRepository.WriteToFile(historicalData, path);
    }
}