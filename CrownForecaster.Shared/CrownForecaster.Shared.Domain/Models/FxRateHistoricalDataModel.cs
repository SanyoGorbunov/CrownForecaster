using System.Text.Json.Serialization;

namespace CrownForecaster.Shared.Domain.Models;

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
