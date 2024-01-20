namespace CrownForecaster.Backend.FxRatesLambda.Tests.FunctionalTests
{
    public class FunctionShould
    {
        [Fact]
        public async Task Execute()
        {
            var lambdaFunction = new Function();

            await lambdaFunction.FunctionHandler(null!, null!);
        }
    }
}
