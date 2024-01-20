using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ExchangeRatesApiClient;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    public class LatestFxRateUpdaterService : ILatestFxRateUpdaterService
    {
        private readonly IExchangeRatesApiClient _exchangeRatesApiClient;

        public LatestFxRateUpdaterService(IExchangeRatesApiClient exchangeRatesApiClient)
        {
            _exchangeRatesApiClient = exchangeRatesApiClient;
        }

        public async Task<FxRateHistoricalData> AddLatestFxRate(FxRateHistoricalData historicalData, string exchangeRatesApiAccessKey)
        {
            decimal latestRate = await _exchangeRatesApiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, exchangeRatesApiAccessKey);

            var updatedFxRates = new List<FxRate>(historicalData.FxRates);
            updatedFxRates.Add(new FxRate(historicalData.LastDate.AddDays(1), latestRate));

            return FxRateHistoricalData.CreateFromFxRates(updatedFxRates);
        }
    }
}
