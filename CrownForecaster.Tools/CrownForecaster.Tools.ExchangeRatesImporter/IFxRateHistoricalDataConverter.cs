using CrownForecaster.Shared.Domain.Models;

namespace CrownForecaster.Tools.ExchangeRatesImporter;

public interface IFxRateHistoricalDataConverter
{
    FxRateHistoricalDataModel ConvertToModel(FxRateHistoricalData historicalData);
}