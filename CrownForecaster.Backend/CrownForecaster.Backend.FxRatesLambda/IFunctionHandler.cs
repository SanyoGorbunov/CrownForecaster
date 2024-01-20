namespace CrownForecaster.Backend.FxRatesLambda
{
    public interface IFunctionHandler
    {
        Task Execute(string exchangeRatesApiAccessKey, int horizon);
    }
}
