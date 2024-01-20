using CrownForecaster.Backend.FxRatesLambda.Services;

namespace CrownForecaster.Backend.FxRatesLambda
{
    public class FunctionHandler : IFunctionHandler
    {
        private readonly IFxRateHistoricalDataRepository _historicalDataRepository;

        public FunctionHandler(IFxRateHistoricalDataRepository historicalDataRepository)
        {
            _historicalDataRepository = historicalDataRepository;
        }

        public Task Execute(string exchangeRatesApiAccessKey, int horizon)
        {
            throw new NotImplementedException();
        }
    }
}
