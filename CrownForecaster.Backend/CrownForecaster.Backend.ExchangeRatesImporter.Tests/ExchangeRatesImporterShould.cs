namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class ExchangeRatesImporterShould
    {
        [Fact]
        public async Task Import()
        {
            var importer = CreateImporter();

            await importer.ImportExchangeRates(new DateOnly(2023, 1, 1), new DateOnly(2024, 1, 1), "somePath.json");
        }

        [Fact]
        public async Task Fail_WhenExchangeRatesApiAccessKeyIsNotPassed()
        {
            Assert.Throws<ArgumentNullException>(() => CreateImporterWithoutAccessKey());
        }

        private static ExchangeRatesImporter CreateImporter()
        {
            return new ExchangeRatesImporter("some-access-key");
        }

        private static ExchangeRatesImporter CreateImporterWithoutAccessKey()
        {
            return new ExchangeRatesImporter(null);
        }
    }
}