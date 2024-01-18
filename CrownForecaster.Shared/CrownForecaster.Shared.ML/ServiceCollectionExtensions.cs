using Microsoft.Extensions.DependencyInjection;

namespace CrownForecaster.Shared.ML;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection RegisterFxForecaster(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFxForecaster, FxForecaster>();

        return serviceCollection;
    }
}
