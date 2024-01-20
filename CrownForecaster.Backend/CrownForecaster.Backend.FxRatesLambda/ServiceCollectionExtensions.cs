using Amazon.S3;
using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.FxRatesLambda
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection RegisterServices(this ServiceCollection services)
        {
            services.RegisterDomainServices();

            services.AddTransient<IAmazonS3, AmazonS3Client>(_ => new AmazonS3Client());

            services.AddSingleton<IFxRateHistoricalDataRepository, FxRateHistoricalDataRepository>();
            services.AddSingleton<IFunctionHandler, FunctionHandler>();

            return services;
        }
    }
}
