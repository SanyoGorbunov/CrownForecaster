using CrownForecaster.Shared.Domain.Models;

namespace CrownForecaster.Shared.Domain.Converters;

public interface IFxRateHistoricalDataConverter
{
    FxRateHistoricalDataModel ConvertToModel(FxRateHistoricalData historicalData);

    FxRateHistoricalData ConvertFromModel(FxRateHistoricalDataModel model);
}