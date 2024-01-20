using CrownForecaster.Shared.Domain.Models;

namespace CrownForecaster.Shared.Domain.Converters;

internal class FxRateHistoricalDataConverter : IFxRateHistoricalDataConverter
{
    private const string DateFormat = "yyyy-MM-dd";

    public FxRateHistoricalData ConvertFromModel(FxRateHistoricalDataModel model)
    {
        return FxRateHistoricalData.CreateFromStatistics(
            DateOnly.ParseExact(model.FirstDate, DateFormat),
            DateOnly.ParseExact(model.LastDate, DateFormat),
            model.Rates.Select(r => (decimal)r),
            model.PredictedFxRate is null ? null : new FxRate(DateOnly.ParseExact(model.PredictedFxRate.Date, DateFormat), (decimal)model.PredictedFxRate.Rate, true));
    }

    public FxRateHistoricalDataModel ConvertToModel(FxRateHistoricalData historicalData)
    {
        return new FxRateHistoricalDataModel
        {
            FirstDate = historicalData.FirstDate.ToString(DateFormat),
            LastDate = historicalData.LastDate.ToString(DateFormat),
            Rates = historicalData.HistoricalFxRates.OrderBy(r => r.Date).Select(r => (double)r.Rate).ToArray(),
            PredictedFxRate = historicalData.PredictedFxRate is null ? null : new FxRateModel
            {
                Date = historicalData.PredictedFxRate.Date.ToString(DateFormat),
                Rate = (double)historicalData.PredictedFxRate.Rate
            }
        };
    }
}
