using Amazon.S3;
using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.ExchangeRatesApiClient;
using CrownForecaster.Shared.ML;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.FxRatesLambda
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection RegisterServices(this ServiceCollection services)
        {
            services.RegisterDomainServices();
            services.RegisterExchangeRatesApiClient();
            services.RegisterFxForecaster();

            services.AddTransient<IAmazonS3, AmazonS3Client>(_ => new AmazonS3Client());

            services.AddSingleton<IFxRateHistoricalDataRepository, FxRateHistoricalDataRepository>();
            services.AddSingleton<IFunctionHandler, FunctionHandler>();
            services.AddSingleton<ILatestFxRateUpdaterService, LatestFxRateUpdaterService>();
            services.AddSingleton<IPredictedFxRateUpdaterService, PredictedFxRateUpdaterService>();

            return services;
        }
    }
}
