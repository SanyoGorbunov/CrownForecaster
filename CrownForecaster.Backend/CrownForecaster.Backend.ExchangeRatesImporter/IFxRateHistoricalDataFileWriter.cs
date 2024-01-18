namespace CrownForecaster.Backend.ExchangeRatesImporter;

public interface IFxRateHistoricalDataFileWriter
{
    Task WriteToFile(FxRateHistoricalData historicalData, string path);
}
