using CrownForecaster.Shared.Domain;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    public interface IPredictedFxRateUpdaterService
    {
        FxRateHistoricalData AddPredictedFxRate(FxRateHistoricalData historicalData, int horizon);
    }
}
