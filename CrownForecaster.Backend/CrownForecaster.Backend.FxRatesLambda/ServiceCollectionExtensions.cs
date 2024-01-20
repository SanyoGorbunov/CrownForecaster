using CrownForecaster.Backend.FxRatesLambda.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.FxRatesLambda
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection RegisterServices(this ServiceCollection services)
        {
            services.AddSingleton<IFxRateHistoricalDataRepository, FxRateHistoricalDataRepository>();
            services.AddSingleton<IFunctionHandler, FunctionHandler>();

            return services;
        }
    }
}
