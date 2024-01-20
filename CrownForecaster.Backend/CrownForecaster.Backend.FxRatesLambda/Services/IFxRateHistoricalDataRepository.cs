using CrownForecaster.Shared.Domain;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    public interface IFxRateHistoricalDataRepository
    {
        Task<FxRateHistoricalData> Get();

        Task Save(FxRateHistoricalData historicalData);
    }
}
