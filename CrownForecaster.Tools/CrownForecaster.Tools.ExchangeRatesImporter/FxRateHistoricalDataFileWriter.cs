using System.Text.Json;

namespace CrownForecaster.Tools.ExchangeRatesImporter;

internal class FxRateHistoricalDataFileWriter : IFxRateHistoricalDataFileWriter
{
    private readonly IFxRateHistoricalDataConverter _converter;

    public FxRateHistoricalDataFileWriter(IFxRateHistoricalDataConverter converter)
    {
        _converter = converter;
    }

    public async Task WriteToFile(FxRateHistoricalData historicalData, string path)
    {
        var model = _converter.ConvertToModel(historicalData);

        using var stream = File.OpenWrite(path);
        await JsonSerializer.SerializeAsync(stream, model);
    }
}
