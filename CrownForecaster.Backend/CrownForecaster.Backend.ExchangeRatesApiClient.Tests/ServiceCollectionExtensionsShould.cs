using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Backend.ExchangeRatesApiClient.Tests;

public class ServiceCollectionExtensionsShould
{
    [Fact]
    public void RegisterExchangeRatesApiClient()
    {
        var serviceProvider = new ServiceCollection()
            .RegisterExchangeRatesApiClient()
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IExchangeRatesApiClient>());
    }
}
