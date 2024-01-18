namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class ExchangeRatesImporterShould
    {
        [Fact]
        public async Task Import()
        {
            var importer = new ExchangeRatesImporter();

            await importer.ImportExchangeRates(new DateOnly(2023, 1, 1), new DateOnly(2024, 1, 1), "somePath.json");
        }
    }
}