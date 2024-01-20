using CrownForecaster.Backend.FxRatesLambda.Services;

namespace CrownForecaster.Backend.FxRatesLambda
{
    public class FunctionHandler : IFunctionHandler
    {
        private readonly IFxRateHistoricalDataRepository _historicalDataRepository;
        private readonly ILatestFxRateUpdaterService _latestFxRateUpdaterService;
        private readonly IPredictedFxRateUpdaterService _predictedFxRateUpdaterService;

        public FunctionHandler(
            IFxRateHistoricalDataRepository historicalDataRepository,
            ILatestFxRateUpdaterService latestFxRateUpdaterService,
            IPredictedFxRateUpdaterService predictedFxRateUpdaterService)
        {
            _historicalDataRepository = historicalDataRepository;
            _latestFxRateUpdaterService = latestFxRateUpdaterService;
            _predictedFxRateUpdaterService = predictedFxRateUpdaterService;
        }

        public async Task Execute(string exchangeRatesApiAccessKey, int horizon)
        {
            var historicalData = await _historicalDataRepository.Get();

            var historicalDataWithLatest = await _latestFxRateUpdaterService.AddLatestFxRate(historicalData, exchangeRatesApiAccessKey);

            var historicalDataWithPredicted = _predictedFxRateUpdaterService.AddPredictedFxRate(historicalDataWithLatest, horizon);

            await _historicalDataRepository.Save(historicalDataWithPredicted);
        }
    }
}
