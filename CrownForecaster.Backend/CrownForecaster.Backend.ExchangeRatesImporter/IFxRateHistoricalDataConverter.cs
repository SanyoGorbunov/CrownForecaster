using System.Text.Json.Serialization;

namespace CrownForecaster.Backend.ExchangeRatesImporter;

public interface IFxRateHistoricalDataConverter
{
    FxRateHistoricalDataModel ConvertToModel(FxRateHistoricalData historicalData);
}

public class FxRateHistoricalDataModel
{
    [JsonPropertyName("firstDate")]
    public string FirstDate { get; set; }

    [JsonPropertyName("lastDate")]
    public string LastDate { get; set; }

    [JsonPropertyName("rates")]
    public double[] Rates { get; set; }

    [JsonPropertyName("predictedFxRate")]
    public FxRateModel? PredictedFxRate { get; set; }
}

public class FxRateModel
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("rate")]
    public double Rate { get; set; }
}