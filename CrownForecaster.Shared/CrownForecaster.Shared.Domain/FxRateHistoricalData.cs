namespace CrownForecaster.Shared.Domain;

public class FxRateHistoricalData
{
    public IEnumerable<FxRate> FxRates { get; private set; }

    public static FxRateHistoricalData CreateFromFxRates(IEnumerable<FxRate> fxRates)
    {
        return new FxRateHistoricalData { FxRates = fxRates };
    }

    public static FxRateHistoricalData CreateFromStatistics(DateOnly firstDate, DateOnly lastDate, IEnumerable<decimal> historicalFxRates, FxRate? predictedFxRate)
    {
        int computedNumberOfDays = lastDate.DayNumber - firstDate.DayNumber + 1;
        if (computedNumberOfDays != historicalFxRates.Count())
        {
            throw new Exception($"First and last dates don't correspond to number of historical rates, {computedNumberOfDays} against {historicalFxRates.Count()}");
        }

        var fxRates = new List<FxRate>();
        for (int i = 0; i < computedNumberOfDays; i++)
        {
            fxRates.Add(new FxRate(firstDate.AddDays(i), historicalFxRates.ElementAt(i)));
        }

        if (predictedFxRate is not null)
        {
            fxRates.Add(predictedFxRate);
        }

        return new FxRateHistoricalData { FxRates = fxRates };
    }

    public DateOnly FirstDate => FxRates.Where(r => !r.IsPredicted).Min(r => r.Date);

    public DateOnly LastDate => FxRates.Where(r => !r.IsPredicted).Max(r => r.Date);

    public FxRate? PredictedFxRate => FxRates.SingleOrDefault(r => r.IsPredicted);

    public IEnumerable<FxRate> HistoricalFxRates => FxRates.Where(r => !r.IsPredicted);
}

public record FxRate(DateOnly Date, decimal Rate, bool IsPredicted = false);