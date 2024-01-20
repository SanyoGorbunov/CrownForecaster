using CrownForecaster.Backend.FxRatesLambda.Services;

namespace CrownForecaster.Backend.FxRatesLambda
{
    public class FunctionHandler : IFunctionHandler
    {
        private readonly IFxRateHistoricalDataRepository _historicalDataRepository;
        private readonly ILatestFxRateUpdaterService _latestFxRateUpdaterService;

        public FunctionHandler(IFxRateHistoricalDataRepository historicalDataRepository, ILatestFxRateUpdaterService latestFxRateUpdaterService)
        {
            _historicalDataRepository = historicalDataRepository;
            _latestFxRateUpdaterService = latestFxRateUpdaterService;
        }

        public async Task Execute(string exchangeRatesApiAccessKey, int horizon)
        {
            var historicalData = await _historicalDataRepository.Get();

            var historicalDataWithLatest = await _latestFxRateUpdaterService.AddLatestFxRate(historicalData, exchangeRatesApiAccessKey);
        }
    }
}
