using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    public class ServiceCollectionExtensionsShould
    {
        [Fact]
        public void RegisterServices()
        {
            var serviceProvider = new ServiceCollection()
                .RegisterServices()
                .BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService<IFunctionHandler>());
        }
    }
}
