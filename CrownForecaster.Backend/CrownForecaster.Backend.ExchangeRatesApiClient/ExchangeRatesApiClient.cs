using System.Net.Http.Json;

namespace CrownForecaster.Backend.ExchangeRatesApiClient;

internal class ExchangeRatesApiClient : IExchangeRatesApiClient
{
    private IHttpClientFactory _httpClientFactory;

    public ExchangeRatesApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<decimal> GetHistoricalFxRateWithBaseEur(CurrencyCode currencyCode, DateOnly date, string accessKey)
    {
        var outputModel = await GetFxRate(date.ToString("yyyy-MM-dd"), currencyCode, accessKey);

        if (date.ToString("yyyy-MM-dd") != outputModel.Date)
        {
            throw new Exception($"Date ({outputModel.Date}) doesn't match requested date {date.ToString("yyyy-MM-dd")}");
        }

        return outputModel.Rates[currencyCode.ToString()];
    }

    public async Task<decimal> GetLatestFxRateWithBaseEur(CurrencyCode currencyCode, string accessKey)
    {
        var outputModel = await GetFxRate("latest", currencyCode, accessKey);

        return outputModel.Rates[currencyCode.ToString()];
    }

    private async Task<ExchangeRatesApiOutputModel> GetFxRate(string endpoint, CurrencyCode currencyCode, string accessKey)
    {
        string requestUri = $"http://api.exchangeratesapi.io/v1/{endpoint}?access_key={accessKey}&symbols={currencyCode}";

        ExchangeRatesApiOutputModel outputModel;
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var parsedOutputModel = await httpClient.GetFromJsonAsync<ExchangeRatesApiOutputModel>(requestUri);
            outputModel = parsedOutputModel ?? throw new Exception("Deserialized to null");
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to retrieve or deserialize call to Exchanges Rates API", ex);
        }

        if (CurrencyCode.EUR.ToString() != outputModel.Base)
        {
            throw new Exception($"Base currency ({outputModel.Base}) doesn't match EUR");
        }

        if (!outputModel.Rates.ContainsKey(currencyCode.ToString()))
        {
            throw new Exception($"Rates don't contain requested currency {currencyCode}");
        }

        return outputModel;
    }
}
