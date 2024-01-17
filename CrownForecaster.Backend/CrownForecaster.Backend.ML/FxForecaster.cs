namespace CrownForecaster.Backend.ML;

internal class FxForecaster : IFxForecaster
{
    public FxRate Forecast(IEnumerable<FxRate> historicalRates, DateOnly forecastDate)
    {
        return new FxRate(forecastDate, 10.23m);
    }
}
