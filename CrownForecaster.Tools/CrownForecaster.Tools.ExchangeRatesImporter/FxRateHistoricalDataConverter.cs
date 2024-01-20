using CrownForecaster.Shared.Domain.Models;

namespace CrownForecaster.Tools.ExchangeRatesImporter;

internal class FxRateHistoricalDataConverter : IFxRateHistoricalDataConverter
{
    private const string DateFormat = "yyyy-MM-dd";

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
