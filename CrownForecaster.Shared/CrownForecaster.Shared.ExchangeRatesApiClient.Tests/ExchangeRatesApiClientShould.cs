using Moq;
using RichardSzalay.MockHttp;
using System.Net;

namespace CrownForecaster.Shared.ExchangeRatesApiClient.Tests
{
    public class ExchangeRatesApiClientShould
    {
        private MockHttpMessageHandler _mockHttpMessageHandler = new MockHttpMessageHandler();

        public class GetHistoricalFxRateShould : ExchangeRatesApiClientShould
        {
            [Fact]
            public async Task Fail_WhenApiRespondsWithInvalidStatusCode()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.InternalServerError);

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task Fail_WhenCanNotDeserializeApiResponse()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Throw(new Exception("from mocked http message handler"));

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task Fail_WhenHttpClientThrows()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "wrong response");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task Fail_WhenBaseCurrencyInResponseIsNotEur()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"USD\",\"date\":\"2024-01-18\",\"rates\":{\"CZK\":24.740948}}");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task Fail_WhenDateInResponseIsNotCorrect()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"EUR\",\"date\":\"3124-11-28\",\"rates\":{\"CZK\":24.740948}}");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task Fail_WhenThereIsNoRequestedCurrency()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"EUR\",\"date\":\"2024-01-18\",\"rates\":{\"USD\":24.740948}}");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task Fail_WhenResponseIsNotSuccess()
            {
                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey"));
            }

            [Fact]
            public async Task GetHistoricalFxRate()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/2024-01-18?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"EUR\",\"date\":\"2024-01-18\",\"rates\":{\"CZK\":24.740948}}");

                var apiClient = CreateApiClient();

                decimal fxRate = await apiClient.GetHistoricalFxRateWithBaseEur(CurrencyCode.CZK, new DateOnly(2024, 1, 18), "testkey");

                Assert.Equal(24.740948m, fxRate);
            }
        }

        public class GetLatestFxRateShould : ExchangeRatesApiClientShould
        {
            [Fact]
            public async Task Fail_WhenApiRespondsWithInvalidStatusCode()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/latest?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.InternalServerError);

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey"));
            }

            [Fact]
            public async Task Fail_WhenCanNotDeserializeApiResponse()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/latest?access_key=testkey&symbols=CZK")
                    .Throw(new Exception("from mocked http message handler"));

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey"));
            }

            [Fact]
            public async Task Fail_WhenHttpClientThrows()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/latest?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "wrong response");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey"));
            }

            [Fact]
            public async Task Fail_WhenBaseCurrencyInResponseIsNotEur()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/latest?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"USD\",\"date\":\"2024-01-18\",\"rates\":{\"CZK\":24.740948}}");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey"));
            }

            [Fact]
            public async Task Fail_WhenThereIsNoRequestedCurrency()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/latest?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"EUR\",\"date\":\"2024-01-18\",\"rates\":{\"USD\":24.740948}}");

                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey"));
            }

            [Fact]
            public async Task Fail_WhenResponseIsNotSuccess()
            {
                var apiClient = CreateApiClient();

                await Assert.ThrowsAsync<Exception>(
                    () => apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey"));
            }

            [Fact]
            public async Task GetHistoricalFxRate()
            {
                _mockHttpMessageHandler
                    .Expect(HttpMethod.Get, "http://api.exchangeratesapi.io/v1/latest?access_key=testkey&symbols=CZK")
                    .Respond(HttpStatusCode.OK, "application/json", "{\"success\":true,\"timestamp\":1705533304,\"base\":\"EUR\",\"date\":\"2024-01-18\",\"rates\":{\"CZK\":24.740948}}");

                var apiClient = CreateApiClient();

                decimal fxRate = await apiClient.GetLatestFxRateWithBaseEur(CurrencyCode.CZK, "testkey");

                Assert.Equal(24.740948m, fxRate);
            }
        }

        private ExchangeRatesApiClient CreateApiClient()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_mockHttpMessageHandler));

            return new ExchangeRatesApiClient(httpClientFactoryMock.Object);
        }
    }
}
