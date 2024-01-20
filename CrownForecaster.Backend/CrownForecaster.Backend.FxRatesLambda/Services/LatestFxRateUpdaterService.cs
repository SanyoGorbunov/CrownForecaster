using CrownForecaster.Shared.Domain;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    public class LatestFxRateUpdaterService : ILatestFxRateUpdaterService
    {
        public Task<FxRateHistoricalData> AddLatestFxRate(FxRateHistoricalData historicalData, string exchangeRatesApiAccessKey)
        {
            throw new NotImplementedException();
        }
    }
}
