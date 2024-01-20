namespace CrownForecaster.Shared.Domain.Tests
{
    public class FxRateHistoricalDataShould
    {
        [Fact]
        public void HaveStatistics_WhenCreatedFromFxRates()
        {
            var fxRates = new FxRate[]
            {
                new FxRate(new DateOnly(2023, 1, 13), 2.24m),
                new FxRate(new DateOnly(2023, 1, 14), 3.24m),
                new FxRate(new DateOnly(2023, 1, 15), 4.24m),
                new FxRate(new DateOnly(2023, 2, 15), 5.24m, true),
            };

            var historicalData = FxRateHistoricalData.CreateFromFxRates(fxRates);

            Assert.Equal(new DateOnly(2023, 1, 13), historicalData.FirstDate);
            Assert.Equal(new DateOnly(2023, 1, 15), historicalData.LastDate);
            Assert.Equal(new FxRate(new DateOnly(2023, 2, 15), 5.24m, true), historicalData.PredictedFxRate);
            Assert.Equal(3, historicalData.HistoricalFxRates.Count());
            Assert.Collection(historicalData.HistoricalFxRates,
                fxRate1 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 13), 2.24m), fxRate1),
                fxRate2 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 14), 3.24m), fxRate2),
                fxRate3 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 15), 4.24m), fxRate3));
        }

        [Fact]
        public void HaveFxRates_WhenCreatedFromStatistics()
        {
            var historicalData = FxRateHistoricalData.CreateFromStatistics(
                new DateOnly(2023, 1, 13),
                new DateOnly(2023, 1, 15),
                new[] { 2.24m, 3.24m, 4.24m },
                new FxRate(new DateOnly(2023, 2, 15), 5.24m, true));

            Assert.Equal(4, historicalData.FxRates.Count());
            Assert.Collection(historicalData.FxRates,
                fxRate1 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 13), 2.24m), fxRate1),
                fxRate2 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 14), 3.24m), fxRate2),
                fxRate3 => Assert.Equal(new FxRate(new DateOnly(2023, 1, 15), 4.24m), fxRate3),
                fxRate4 => Assert.Equal(new FxRate(new DateOnly(2023, 2, 15), 5.24m, true), fxRate4));
        }
    }
}
