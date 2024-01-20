using CrownForecaster.Backend.FxRatesLambda.Services;
using Moq;

namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    public class FunctionHandlerShould
    {
        private readonly Mock<IFxRateHistoricalDataRepository> _historicalDataRepositoryMock = new Mock<IFxRateHistoricalDataRepository>();

        [Fact]
        public async Task Execute()
        {
            var handler = CreateHandler();

            await handler.Execute(null!, 30);
        }

        private FunctionHandler CreateHandler()
        {
            return new FunctionHandler(_historicalDataRepositoryMock.Object);
        }
    }
}