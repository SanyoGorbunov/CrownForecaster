using Amazon.S3;
using Amazon.S3.Model;
using CrownForecaster.Shared.ExchangeRatesApiClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;

namespace CrownForecaster.Backend.FxRatesLambda.Tests.FunctionalTests
{
    public class FunctionShould
    {
        private readonly Mock<IAmazonS3> _amazonS3Mock = new Mock<IAmazonS3>();
        private readonly Mock<IExchangeRatesApiClient> _exchangeRatesApiClientMock = new Mock<IExchangeRatesApiClient>();

        [Fact]
        public async Task Execute()
        {
            var lambdaFunction = new Function();
            lambdaFunction.ServiceCollection.Replace(ServiceDescriptor.Singleton<IAmazonS3>(_amazonS3Mock.Object));
            lambdaFunction.ServiceCollection.Replace(ServiceDescriptor.Singleton<IExchangeRatesApiClient>(_exchangeRatesApiClientMock.Object));

            using var responseStream = await TestHelpers.StubStreamWithText("{\"firstDate\":\"2023-01-01\",\"lastDate\":\"2023-01-01\",\"rates\":[1]}");
            _amazonS3Mock
                .Setup(_ => _.GetObjectAsync(
                    It.Is<GetObjectRequest>(r =>
                        r.BucketName == "crown-forecaster-historical-fx-rates" &&
                        r.Key == "historicalFxRates.json"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetObjectResponse { ResponseStream = responseStream })
                .Verifiable();
            _amazonS3Mock
                .Setup(_ => _.PutObjectAsync(
                    It.Is<PutObjectRequest>(r =>
                        r.BucketName == "crown-forecaster-historical-fx-rates" &&
                        r.Key == "historicalFxRates.json" &&
                        MatchInputStream(r.InputStream)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK })
                .Verifiable();
            _exchangeRatesApiClientMock.Setup(_ => _.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "some-access-key"))
                .ReturnsAsync(2m).Verifiable();

            Environment.SetEnvironmentVariable("EXCHANGE_RATES_API_ACCESS_KEY", "some-access-key");
            Environment.SetEnvironmentVariable("PREDICTION_HORIZON", "5");
            await lambdaFunction.FunctionHandler(null!, null!);

            _amazonS3Mock.VerifyAll();
            _exchangeRatesApiClientMock.VerifyAll();
        }

        private bool MatchInputStream(Stream inputStream)
        {
            throw new NotImplementedException();
        }
    }
}
