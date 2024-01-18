namespace CrownForecaster.Backend.ExchangeRatesImporter;

public interface IFxRateRepository
{
    Task SaveFxRates(IEnumerable<FxRate> fxRates);
}

public record FxRate(DateOnly Date, decimal Rate);