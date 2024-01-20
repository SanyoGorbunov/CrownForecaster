using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ML;
using FxRate = CrownForecaster.Shared.Domain.FxRate;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    internal class PredictedFxRateUpdaterService : IPredictedFxRateUpdaterService
    {
        private readonly IFxForecaster _fxForecaster;

        public PredictedFxRateUpdaterService(IFxForecaster fxForecaster)
        {
            _fxForecaster = fxForecaster;
        }

        public FxRateHistoricalData AddPredictedFxRate(FxRateHistoricalData historicalData, int horizon)
        {
            var fxRates = new List<FxRate>(historicalData.HistoricalFxRates);

            var predictedFxRate = _fxForecaster.Forecast(fxRates.Select(r => new Shared.ML.FxRate
            {
                Date = r.Date.ToDateTime(TimeOnly.MinValue),
                Rate = (float)r.Rate
            }), horizon);

            fxRates.Add(new FxRate(DateOnly.FromDateTime(predictedFxRate.Date), (decimal)predictedFxRate.Rate, true));

            return FxRateHistoricalData.CreateFromFxRates(fxRates);
        }
    }
}
