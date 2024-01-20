using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ML;
using Moq;
using FxRate = CrownForecaster.Shared.Domain.FxRate;

namespace CrownForecaster.Backend.FxRatesLambda.Tests.Services
{
    public class PredictedFxRateUpdaterServiceShould
    {
        private readonly Mock<IFxForecaster> _fxForecasterMock = new Mock<IFxForecaster>();

        [Fact]
        public void AddPredictedFxRate()
        {
            var fxRates = new[] {
                new FxRate(new DateOnly(2023, 1, 1), 3.25m),
                new FxRate(new DateOnly(2023, 1, 2), 3.35m, true),
            };
            var historicalData = FxRateHistoricalData.CreateFromFxRates(fxRates);

            _fxForecasterMock.Setup(_ => _.Forecast(
                It.Is<IEnumerable<Shared.ML.FxRate>>(rs =>
                    rs.Count() == 1 &&
                    rs.ElementAt(0).Date == new DateOnly(2023, 1, 1).ToDateTime(TimeOnly.MinValue) &&
                    rs.ElementAt(0).Rate == 3.25f), 30))
                .Returns(new Shared.ML.FxRate { Date = new DateTime(2023, 1, 31), Rate = 5.25f })
                .Verifiable();

            var service = new PredictedFxRateUpdaterService(_fxForecasterMock.Object);

            var updatedHistoricalData = service.AddPredictedFxRate(historicalData, 30);

            Assert.Equal(2, updatedHistoricalData.FxRates.Count());
            Assert.Collection(updatedHistoricalData.FxRates,
                fxRate1 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 1), 3.25m), fxRate1),
                fxRate2 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 31), 5.25m, true), fxRate2));
            _fxForecasterMock.VerifyAll();
        }
    }
}
