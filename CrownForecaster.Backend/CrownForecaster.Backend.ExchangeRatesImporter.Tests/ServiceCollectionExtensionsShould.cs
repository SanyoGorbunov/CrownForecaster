using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class ServiceCollectionExtensionsShould
    {
        [Fact]
        public void RegisterExchangeRatesImporter()
        {
            var serviceProvider = new ServiceCollection()
                .RegisterExchangeRatesImporter()
                .BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService<IExchangeRatesImporter>());
        }
    }
}