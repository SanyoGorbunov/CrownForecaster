using CrownForecaster.Backend.ExchangeRatesApiClient;
using CrownForecaster.Backend.ML;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .RegisterFxForecaster()
    .RegisterExchangeRatesApiClient()
    .BuildServiceProvider();

var fxForecaster = serviceProvider.GetRequiredService<IFxForecaster>();

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