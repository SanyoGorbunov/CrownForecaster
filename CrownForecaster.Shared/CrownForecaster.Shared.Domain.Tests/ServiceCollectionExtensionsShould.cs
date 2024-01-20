using CrownForecaster.Shared.Domain.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Shared.Domain.Tests;

public class ServiceCollectionExtensionsShould
{
    [Fact]
    public void RegisterExchangeRatesApiClient()
    {
        var serviceProvider = new ServiceCollection()
            .RegisterDomainServices()
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IFxRateHistoricalDataConverter>());
    }
}
