using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using Moq;

namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    public class FunctionHandlerShould
    {
        private readonly Mock<IFxRateHistoricalDataRepository> _historicalDataRepositoryMock = new Mock<IFxRateHistoricalDataRepository>();

        [Fact]
        public async Task Execute()
        {
            var historicalData = FxRateHistoricalData.CreateFromFxRates(null!);

            _historicalDataRepositoryMock.Setup(_ => _.Get()).ReturnsAsync(historicalData).Verifiable();
            var handler = CreateHandler();

            await handler.Execute(null!, 30);

            _historicalDataRepositoryMock.VerifyAll();
        }

        private FunctionHandler CreateHandler()
        {
            return new FunctionHandler(_historicalDataRepositoryMock.Object);
        }
    }
}