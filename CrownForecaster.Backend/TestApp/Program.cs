using CrownForecaster.Backend.ML;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .RegisterFxForecaster()
    .BuildServiceProvider();

var fxForecaster = serviceProvider.GetRequiredService<IFxForecaster>();

IEnumerable<FxRate> GenerateFxRates()
{
    decimal baselineValue = 25.5m;
    var random = new Random();

    DateOnly startDate = DateOnly.FromDateTime(DateTime.UtcNow).AddMonths(-3);
    DateOnly endDate = DateOnly.FromDateTime(DateTime.UtcNow);

    var fxRates = new List<FxRate>();
    while (startDate <= endDate)
    {
        fxRates.Add(new FxRate(startDate, baselineValue + (decimal)((random.NextDouble() - 0.5) * 3)));
        startDate = startDate.AddDays(1);
    }

    return fxRates;
}

var forecastedFxRate = fxForecaster.Forecast(GenerateFxRates(), DateOnly.FromDateTime(DateTime.UtcNow).AddMonths(1));

Console.WriteLine(forecastedFxRate);