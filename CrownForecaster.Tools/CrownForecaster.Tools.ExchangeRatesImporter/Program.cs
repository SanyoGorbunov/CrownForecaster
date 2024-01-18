using CrownForecaster.Tools.ExchangeRatesImporter;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .RegisterExchangeRatesImporter()
    .BuildServiceProvider();

var importer = serviceProvider.GetRequiredService<IExchangeRatesImporter>();

//DateOnly startDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
DateOnly startDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-1);
DateOnly endDate = DateOnly.FromDateTime(DateTime.Now);
string path = "historicalFxRates.json";

await importer.ImportExchangeRates(startDate, endDate, path);