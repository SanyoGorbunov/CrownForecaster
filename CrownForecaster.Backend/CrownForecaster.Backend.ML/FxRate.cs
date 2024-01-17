namespace CrownForecaster.Backend.ML;

public class FxRate
{
    public DateTime Date { get; set; }

    public float Rate { get; set; }
};

public class FxRateForecast
{
    public float[] Forecast { get; set; }
}