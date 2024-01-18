namespace CrownForecaster.Backend.ExchangeRatesImporter;

public interface IExchangeRatesImporter
{
    Task ImportExchangeRates(DateOnly startDate, DateOnly endDate, string path);
}
