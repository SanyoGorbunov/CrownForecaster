using Amazon.S3;
using Amazon.S3.Model;
using CrownForecaster.Backend.FxRatesLambda.Services;
using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.Domain.Converters;
using CrownForecaster.Shared.Domain.Models;
using Moq;
using System.Net;
using System.Text.Json;

namespace CrownForecaster.Backend.FxRatesLambda.Tests.Services
{
    public class FxRateHistoricalDataRepositoryShould
    {
        private readonly Mock<IAmazonS3> _amazonS3Mock = new Mock<IAmazonS3>();
        private readonly Mock<IFxRateHistoricalDataConverter> _converterMock = new Mock<IFxRateHistoricalDataConverter>();

        [Fact]
        public async Task Get()
        {
            using var responseStream = await TestHelpers.StubStreamWithText("{\"firstDate\":\"2023-01-01\",\"lastDate\":\"2023-01-01\",\"rates\":[1]}");
            _amazonS3Mock
                .Setup(_ => _.GetObjectAsync(
                    It.Is<GetObjectRequest>(r =>
                        r.BucketName == "crown-forecaster-historical-fx-rates" &&
                        r.Key == "historicalFxRates.json"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetObjectResponse { ResponseStream = responseStream })
                .Verifiable();
            var historicalData = FxRateHistoricalData.CreateFromFxRates(null!);
            _converterMock.Setup(_ => _.ConvertFromModel(
                It.Is<FxRateHistoricalDataModel>(m => m.FirstDate == "2023-01-01" && m.LastDate == "2023-01-01" && m.Rates[0] == 1d)))
                .Returns(historicalData).Verifiable();
            var repository = CreateRepository();

            var result = await repository.Get();

            Assert.Equal(result, historicalData);
            _amazonS3Mock.VerifyAll();
            _converterMock.VerifyAll();
        }

        [Fact]
        public async Task Save()
        {
            var historicalData = FxRateHistoricalData.CreateFromFxRates(null!);
            var model = new FxRateHistoricalDataModel
            {
                FirstDate = "2023-01-01",
                LastDate = "2023-01-01",
                Rates = new[] { 1d }
            };

            _converterMock.Setup(_ => _.ConvertToModel(historicalData))
                .Returns(model)
                .Verifiable();
            _amazonS3Mock
                .Setup(_ => _.PutObjectAsync(
                    It.Is<PutObjectRequest>(r =>
                        r.BucketName == "crown-forecaster-historical-fx-rates" &&
                        r.Key == "historicalFxRates.json" &&
                        MatchInputStream(r.InputStream)), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK })
                .Verifiable();
            var repository = CreateRepository();

            await repository.Save(historicalData);

            _amazonS3Mock.VerifyAll();
            _converterMock.VerifyAll();
        }

        private bool MatchInputStream(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(stream);
            var serialized = streamReader.ReadToEnd();

            return serialized == "{\"firstDate\":\"2023-01-01\",\"lastDate\":\"2023-01-01\",\"rates\":[1],\"predictedFxRate\":null}";
        }

        private FxRateHistoricalDataRepository CreateRepository()
        {
            return new FxRateHistoricalDataRepository(_amazonS3Mock.Object, _converterMock.Object);
        }
    }
}
