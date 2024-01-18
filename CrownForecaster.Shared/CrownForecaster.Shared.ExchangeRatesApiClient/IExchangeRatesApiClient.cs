namespace CrownForecaster.Shared.ExchangeRatesApiClient;

public interface IExchangeRatesApiClient
{
    Task<decimal> GetHistoricalFxRateWithBaseEur(CurrencyCode currencyCode, DateOnly date, string accessKey);

    Task<decimal> GetLatestFxRateWithBaseEur(CurrencyCode currencyCode, string accessKey);
}
