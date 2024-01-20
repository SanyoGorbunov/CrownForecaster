using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ExchangeRatesApiClient;
using Moq;

namespace CrownForecaster.Backend.FxRatesLambda.Tests.Services
{
    public class LatestFxRateUpdaterServiceShould
    {
        private readonly Mock<IExchangeRatesApiClient> _exchangeRatesApiClientMock = new Mock<IExchangeRatesApiClient>();

        [Fact]
        public async Task AddLatestFxRate()
        {
            var fxRates = new[] { new FxRate(new DateOnly(2023, 1, 1), 3.25m) };
            var historicalData = FxRateHistoricalData.CreateFromFxRates(fxRates);
            var service = CreateService();

            _exchangeRatesApiClientMock.Setup(_ => _.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "some-access-key"))
                .ReturnsAsync(4.25m)
                .Verifiable();

            var updatedHistoricalData = await service.AddLatestFxRate(historicalData, "some-access-key");

            Assert.Equal(2, updatedHistoricalData.FxRates.Count());
            Assert.Collection(updatedHistoricalData.FxRates,
                fxRate1 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 1), 3.25m), fxRate1),
                fxRate2 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 2), 4.25m), fxRate2));
            _exchangeRatesApiClientMock.VerifyAll();
        }

        private LatestFxRateUpdaterService CreateService()
        {
            return new LatestFxRateUpdaterService(_exchangeRatesApiClientMock.Object);
        }
    }
}
