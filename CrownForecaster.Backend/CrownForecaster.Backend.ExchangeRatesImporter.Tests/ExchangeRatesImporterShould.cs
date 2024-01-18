using CrownForecaster.Backend.ExchangeRatesApiClient;
using Moq;

namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class ExchangeRatesImporterShould
    {
        private const string _testAccessKey = "some-access-key";

        private Mock<IExchangeRatesApiClient> _apiClientMock = new Mock<IExchangeRatesApiClient>(MockBehavior.Strict);
        private Mock<IFxRateRepository> _fxRateRepository = new Mock<IFxRateRepository>();

        [Fact]
        public async Task Import()
        {
            var importer = CreateImporter();

            var mockSequence = new MockSequence();
            _apiClientMock.InSequence(mockSequence).Setup(_ => _.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2023, 1, 3), _testAccessKey)).ReturnsAsync(24.5m);
            _apiClientMock.InSequence(mockSequence).Setup(_ => _.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2023, 1, 4), _testAccessKey)).ReturnsAsync(25.5m);
            _apiClientMock.InSequence(mockSequence).Setup(_ => _.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2023, 1, 5), _testAccessKey)).ReturnsAsync(26.5m);

            await importer.ImportExchangeRates(new DateOnly(2023, 1, 3), new DateOnly(2023, 1, 5), "somePath.json");

            _apiClientMock.VerifyAll();
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
            return new ExchangeRatesImporter(_apiClientMock.Object, _fxRateRepository.Object, _testAccessKey);
        }

        private ExchangeRatesImporter CreateImporterWithoutAccessKey()
        {
            return new ExchangeRatesImporter(_apiClientMock.Object, _fxRateRepository.Object, null);
        }
    }
}