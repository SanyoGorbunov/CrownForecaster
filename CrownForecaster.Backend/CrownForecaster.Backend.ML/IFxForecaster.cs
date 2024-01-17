namespace CrownForecaster.Backend.ML;

public interface IFxForecaster
{
    FxRate Forecast(IEnumerable<FxRate> historicalRates, int horizon);
}