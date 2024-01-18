using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace CrownForecaster.Shared.ML;

internal class FxForecaster : IFxForecaster
{
    public FxRate Forecast(IEnumerable<FxRate> historicalRates, int horizon)
    {
        var context = new MLContext();

        var data = context.Data.LoadFromEnumerable(historicalRates);

        var pipeline = context.Forecasting.ForecastBySsa(
            "Forecast",
            nameof(FxRate.Rate),
            windowSize: 5,
            seriesLength: 10,
            trainSize: historicalRates.Count(),
            horizon: horizon);

        var model = pipeline.Fit(data);

        var forecastingEngine = model.CreateTimeSeriesEngine<FxRate, FxRateForecast>(context);

        var forecasts = forecastingEngine.Predict();

        return new FxRate {
            Date = historicalRates.Max(fxRate => fxRate.Date).AddDays(horizon),
            Rate = forecasts.Forecast[horizon - 1]
        };
    }
}
