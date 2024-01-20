using CrownForecaster.Shared.Domain.Models;
using CrownForecaster.Shared.ExchangeRatesApiClient;
using CrownForecaster.Shared.ML;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

var serviceProvider = new ServiceCollection()
    .RegisterFxForecaster()
    .RegisterExchangeRatesApiClient()
    .BuildServiceProvider();

var fxForecaster = serviceProvider.GetRequiredService<IFxForecaster>();

using var httpClient = new HttpClient();
var model = await httpClient.GetFromJsonAsync<FxRateHistoricalDataModel>("https://crown-forecaster-historical-fx-rates.s3.eu-west-1.amazonaws.com/historicalFxRates.json");

IEnumerable<FxRate> GenerateFxRates()
{
    float baselineValue = 25.5f;
    var random = new Random();

    DateOnly startDate = DateOnly.FromDateTime(DateTime.UtcNow).AddMonths(-3);
    DateOnly endDate = DateOnly.FromDateTime(DateTime.UtcNow);

    var fxRates = new List<FxRate>();
    while (startDate <= endDate)
    {
        fxRates.Add(new FxRate {
            Date = startDate.ToDateTime(TimeOnly.MinValue),
            Rate = baselineValue + (float)((random.NextDouble() - 0.5) * 3)
        });
        startDate = startDate.AddDays(1);
    }

    return fxRates;
}

var forecastedFxRate = fxForecaster.Forecast(GenerateFxRates(), 31);

Console.WriteLine(forecastedFxRate);