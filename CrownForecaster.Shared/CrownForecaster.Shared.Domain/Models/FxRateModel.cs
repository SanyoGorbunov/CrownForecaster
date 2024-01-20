using System.Text.Json.Serialization;

namespace CrownForecaster.Shared.Domain.Models;

public class FxRateModel
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("rate")]
    public double Rate { get; set; }
}