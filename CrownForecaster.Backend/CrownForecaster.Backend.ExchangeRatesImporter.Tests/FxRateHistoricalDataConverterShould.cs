namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class FxRateHistoricalDataConverterShould
    {
        [Fact]
        public void ConvertToModel()
        {
            var fxRates = new FxRate[]
            {
                new FxRate(new DateOnly(2023, 1, 13), 2.24m),
                new FxRate(new DateOnly(2023, 1, 14), 3.24m),
                new FxRate(new DateOnly(2023, 1, 15), 4.24m),
                new FxRate(new DateOnly(2023, 2, 15), 5.24m, true),
            };
            var historicalData = FxRateHistoricalData.CreateFromFxRates(fxRates);

            var converter = new FxRateHistoricalDataConverter();

            var model = converter.ConvertToModel(historicalData);

            Assert.Equal("2023-01-13", model.FirstDate);
            Assert.Equal("2023-01-15", model.LastDate);
            Assert.NotNull(model.PredictedFxRate);
            Assert.Equal("2023-02-15", model.PredictedFxRate.Date);
            Assert.Equal(5.24, model.PredictedFxRate.Rate);
            Assert.Equal(new[] { 2.24, 3.24, 4.24 }, model.Rates);
        }
    }
}
