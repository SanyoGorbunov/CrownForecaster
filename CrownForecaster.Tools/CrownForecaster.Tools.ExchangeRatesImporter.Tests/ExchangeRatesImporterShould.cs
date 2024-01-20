using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ExchangeRatesApiClient;
using Moq;

namespace CrownForecaster.Tools.ExchangeRatesImporter.Tests
{
    public class ExchangeRatesImporterShould
    {
        private const string _testAccessKey = "some-access-key";

        private Mock<IExchangeRatesApiClient> _apiClientMock = new Mock<IExchangeRatesApiClient>(MockBehavior.Strict);
        private Mock<IFxRateHistoricalDataFileWriter> _fxRateRepository = new Mock<IFxRateHistoricalDataFileWriter>();

        [Fact]
        public async Task Import()
        {
            var importer = CreateImporter();

            var mockSequence = new MockSequence();
            _apiClientMock.InSequence(mockSequence).Setup(_ => _.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2023, 1, 3), _testAccessKey)).ReturnsAsync(24.5m);
            _apiClientMock.InSequence(mockSequence).Setup(_ => _.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2023, 1, 4), _testAccessKey)).ReturnsAsync(25.5m);
            _apiClientMock.InSequence(mockSequence).Setup(_ => _.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2023, 1, 5), _testAccessKey)).ReturnsAsync(26.5m);

            var capturedHistoricalData = new List<FxRateHistoricalData>();
            _fxRateRepository.Setup(_ => _.WriteToFile(Capture.In(capturedHistoricalData), "somePath.json")).Verifiable();

            await importer.ImportExchangeRates(new DateOnly(2023, 1, 3), new DateOnly(2023, 1, 5), "somePath.json");

            _apiClientMock.VerifyAll();
            _fxRateRepository.VerifyAll();
            Assert.NotNull(capturedHistoricalData[0]);
            Assert.Equal(3, capturedHistoricalData[0].FxRates.Count());
            Assert.Collection(capturedHistoricalData[0].FxRates,
                fxRate1 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 3), 24.5m), fxRate1),
                fxRate2 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 4), 25.5m), fxRate2),
                fxRate3 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 5), 26.5m), fxRate3));
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