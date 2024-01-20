// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
using Amazon.Lambda.CloudWatchEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace CrownForecaster.Backend.FxRatesLambda;

public class Function
{
    private ServiceCollection _serviceCollection;

    public Function()
    {
        _serviceCollection = new ServiceCollection()
            .RegisterServices();
    }

    internal ServiceCollection ServiceCollection => _serviceCollection;

    public async Task FunctionHandler(CloudWatchEvent<object> input, ILambdaContext context)
    {
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var handler = serviceProvider.GetRequiredService<IFunctionHandler>();

        string exchangeRatesApiAccessKey = Environment.GetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY") ?? throw new Exception("EXCHANGE_RATES_API_ACCESS_KEY is not set");
        int horizon = int.TryParse(Environment.GetEnvironmentVariable("PREDICTION_HORIZON"), out horizon) ? horizon : throw new Exception("PREDICTION_HORIZON is not set");
        await handler.Execute(exchangeRatesApiAccessKey, horizon);
    }
}
