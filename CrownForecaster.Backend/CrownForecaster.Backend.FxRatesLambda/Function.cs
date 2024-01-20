// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
using Amazon.Lambda.CloudWatchEvents;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace CrownForecaster.Backend.FxRatesLambda;

public class Function
{
    public async Task FunctionHandler(CloudWatchEvent<object> input, ILambdaContext context)
    {
    }
}
