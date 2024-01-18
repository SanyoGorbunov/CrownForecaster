using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.ExchangeRatesImporter.Tests
{
    public class ServiceCollectionExtensionsShould
    {
        [Fact]
        public void RegisterExchangeRatesImporter()
        {
            Environment.SetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY", "some-access-key");

            var serviceProvider = new ServiceCollection()
                .RegisterExchangeRatesImporter()
                .BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService<IExchangeRatesImporter>());
        }
    }
}