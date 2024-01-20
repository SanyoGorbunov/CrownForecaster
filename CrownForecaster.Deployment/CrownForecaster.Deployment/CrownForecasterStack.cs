using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace CrownForecaster.Deployment
{
    public class CrownForecasterStack : Stack
    {
        internal CrownForecasterStack(Construct scope, string id)
        {
            var historicalFxRatesBucket = new Bucket(this, "HistoricalFxRatesBucket", new BucketProps
            {
                BucketName = "crown-forecaster-historical-fx-rates",
                AccessControl = BucketAccessControl.PUBLIC_READ,
                BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions
                {
                    BlockPublicAcls = false,
                    BlockPublicPolicy = false,
                    IgnorePublicAcls = false,
                    RestrictPublicBuckets = false
                })
            });
        }
    }
}
