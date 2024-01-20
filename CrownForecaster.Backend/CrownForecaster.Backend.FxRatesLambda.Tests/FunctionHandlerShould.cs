using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using Moq;

namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    public class FunctionHandlerShould
    {
        private readonly Mock<IFxRateHistoricalDataRepository> _historicalDataRepositoryMock = new Mock<IFxRateHistoricalDataRepository>();
        private readonly Mock<ILatestFxRateUpdaterService> _latestFxRateUpdaterServiceMock = new Mock<ILatestFxRateUpdaterService>();

        [Fact]
        public async Task Execute()
        {
            var historicalData = FxRateHistoricalData.CreateFromFxRates(null!);
            var historicalDataWithLatest = FxRateHistoricalData.CreateFromFxRates(null!);

            _historicalDataRepositoryMock.Setup(_ => _.Get()).ReturnsAsync(historicalData).Verifiable();
            _latestFxRateUpdaterServiceMock.Setup(_ => _.AddLatestFxRate(historicalData, "some-access-key")).ReturnsAsync(historicalDataWithLatest).Verifiable();
            var handler = CreateHandler();

            await handler.Execute("some-access-key", 30);

            _historicalDataRepositoryMock.VerifyAll();
            _latestFxRateUpdaterServiceMock.VerifyAll();
        }

        private FunctionHandler CreateHandler()
        {
            return new FunctionHandler(_historicalDataRepositoryMock.Object, _latestFxRateUpdaterServiceMock.Object);
        }
    }
}