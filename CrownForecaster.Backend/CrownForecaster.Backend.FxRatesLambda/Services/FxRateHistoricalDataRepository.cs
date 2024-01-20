using Amazon.S3;
using Amazon.S3.Model;
using CrownForecaster.Shared.Domain;
using CrownForecaster.Shared.Domain.Converters;
using CrownForecaster.Shared.Domain.Models;
using System.Text.Json;

namespace CrownForecaster.Backend.FxRatesLambda.Services
{
    internal class FxRateHistoricalDataRepository : IFxRateHistoricalDataRepository
    {
        private const string BucketName = "crown-forecaster-historical-fx-rates";
        private const string ObjectKey = "historicalFxRates.json";

        private readonly IAmazonS3 _amazonS3;
        private readonly IFxRateHistoricalDataConverter _converter;

        public FxRateHistoricalDataRepository(IAmazonS3 amazonS3, IFxRateHistoricalDataConverter converter)
        {
            _amazonS3 = amazonS3;
            _converter = converter;
        }

        public async Task<FxRateHistoricalData> Get()
        {
            var getObjectResponse = await _amazonS3.GetObjectAsync(new GetObjectRequest
            {
                BucketName = BucketName,
                Key = ObjectKey
            });

            var model = await JsonSerializer.DeserializeAsync<FxRateHistoricalDataModel>(getObjectResponse.ResponseStream) ?? throw new Exception("Failed to deserialize model");
            var historicalData = _converter.ConvertFromModel(model);

            return historicalData;
        }

        public async Task Save(FxRateHistoricalData historicalData)
        {
            var model = _converter.ConvertToModel(historicalData);

            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, model);

            var putObjectRequest = new PutObjectRequest { BucketName = BucketName, Key = ObjectKey, InputStream = stream };
            await _amazonS3.PutObjectAsync(putObjectRequest);
        }
    }
}
