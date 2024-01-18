namespace CrownForecaster.Backend.ExchangeRatesImporter;

public class FxRateHistoricalData
{
    public IEnumerable<FxRate> FxRates { get; private set; }

    public static FxRateHistoricalData CreateFromFxRates(IEnumerable<FxRate> fxRates)
    {
        return new FxRateHistoricalData { FxRates = fxRates };
    }

    public DateOnly FirstDate => FxRates.Where(r => !r.IsPredicted).Min(r => r.Date);

    public DateOnly LastDate => FxRates.Where(r => !r.IsPredicted).Max(r => r.Date);

    public FxRate? PredictedFxRate => FxRates.SingleOrDefault(r => r.IsPredicted);

    public IEnumerable<FxRate> HistoricalFxRates => FxRates.Where(r => !r.IsPredicted);
}

public record FxRate(DateOnly Date, decimal Rate, bool IsPredicted = false);