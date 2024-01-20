using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    public class FunctionShould
    {
        private readonly Mock<IFunctionHandler> _functionHandlerMock = new Mock<IFunctionHandler>();

        [Fact]
        public async Task ExecuteHandler()
        {
            var function = new Function();
            function.ServiceCollection.Replace(ServiceDescriptor.Singleton<IFunctionHandler>(_functionHandlerMock.Object));

            Environment.SetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY", "some-access-key");
            Environment.SetEnvironmentVariable("PREDICTION_HORIZON", "5");
            await function.FunctionHandler(null!, null!);

            _functionHandlerMock.Verify(_ => _.Execute("some-access-key", 5));
        }
    }
}
