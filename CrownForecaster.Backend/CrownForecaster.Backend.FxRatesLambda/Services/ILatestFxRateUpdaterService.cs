using CrownForecaster.Shared.Domain;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    public interface ILatestFxRateUpdaterService
    {
        Task<FxRateHistoricalData> AddLatestFxRate(FxRateHistoricalData historicalData, string exchangeRatesApiAccessKey);
    }
}
