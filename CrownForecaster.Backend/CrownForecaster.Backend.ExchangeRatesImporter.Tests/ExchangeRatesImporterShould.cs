using CrownForecaster.Backend.ExchangeRatesApiClient;
using Moq;

namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class ExchangeRatesImporterShould
    {
        private Mock<IExchangeRatesApiClient> _apiClientMock = new Mock<IExchangeRatesApiClient>();

        [Fact]
        public async Task Import()
        {
            var importer = CreateImporter();

            await importer.ImportExchangeRates(new DateOnly(2023, 1, 1), new DateOnly(2024, 1, 1), "somePath.json");
        }

        [Fact]
        public async Task Fail_WhenEndDateIsEarlierThanStartDate()
        {
            var importer = CreateImporter();

            await Assert.ThrowsAsync<ArgumentException>(() => importer.ImportExchangeRates(new DateOnly(2024, 1, 1), new DateOnly(2023, 1, 1), "somePath.json"));
        }

        [Fact]
        public void Fail_WhenExchangeRatesApiAccessKeyIsNotPassed()
        {
            Assert.Throws<ArgumentNullException>(() => CreateImporterWithoutAccessKey());
        }

        private ExchangeRatesImporter CreateImporter()
        {
            return new ExchangeRatesImporter(_apiClientMock.Object, "some-access-key");
        }

        private ExchangeRatesImporter CreateImporterWithoutAccessKey()
        {
            return new ExchangeRatesImporter(_apiClientMock.Object, null);
        }
    }
}