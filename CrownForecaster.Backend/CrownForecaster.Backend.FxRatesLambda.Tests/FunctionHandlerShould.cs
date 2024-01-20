using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using Moq;

namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    public class FunctionHandlerShould
    {
        private readonly Mock<IFxRateHistoricalDataRepository> _historicalDataRepositoryMock = new Mock<IFxRateHistoricalDataRepository>();
        private readonly Mock<ILatestFxRateUpdaterService> _latestFxRateUpdaterServiceMock = new Mock<ILatestFxRateUpdaterService>();
        private readonly Mock<IPredictedFxRateUpdaterService> _predictedFxRateUpdaterService = new Mock<IPredictedFxRateUpdaterService>();

        [Fact]
        public async Task Execute()
        {
            var historicalData = FxRateHistoricalData.CreateFromFxRates(null!);
            var historicalDataWithLatest = FxRateHistoricalData.CreateFromFxRates(null!);
            var historicalDataWithPredicted = FxRateHistoricalData.CreateFromFxRates(null!);

            _historicalDataRepositoryMock.Setup(_ => _.Get()).ReturnsAsync(historicalData).Verifiable();
            _historicalDataRepositoryMock.Setup(_ => _.Save(historicalDataWithPredicted)).Verifiable();
            _latestFxRateUpdaterServiceMock.Setup(_ => _.AddLatestFxRate(historicalData, "some-access-key")).ReturnsAsync(historicalDataWithLatest).Verifiable();
            _predictedFxRateUpdaterService.Setup(_ => _.AddPredictedFxRate(historicalDataWithLatest, 30)).Returns(historicalDataWithPredicted).Verifiable();
            var handler = CreateHandler();

            await handler.Execute("some-access-key", 30);

            _historicalDataRepositoryMock.VerifyAll();
            _latestFxRateUpdaterServiceMock.VerifyAll();
            _predictedFxRateUpdaterService.VerifyAll();
        }

        private FunctionHandler CreateHandler()
        {
            return new FunctionHandler(_historicalDataRepositoryMock.Object, _latestFxRateUpdaterServiceMock.Object, _predictedFxRateUpdaterService.Object);
        }
    }
}