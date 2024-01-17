namespace CrownForecaster.Backend.ML;

public interface IFxForecaster
{
    FxRate Forecast(IEnumerable<FxRate> historicalRates, DateOnly forecastDate);
}