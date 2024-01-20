using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace CrownForecaster.Deployment
{
    public class CrownForecasterStack : Stack
    {
        internal CrownForecasterStack(Construct scope, string id): base(scope, id)
        {
            string bucketName = "crown-forecaster-historical-fx-rates";
            new Bucket(this, "HistoricalFxRatesBucket", new BucketProps
            {
                BucketName = bucketName,
                BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions
                {
                    BlockPublicAcls = false,
                    BlockPublicPolicy = false,
                    IgnorePublicAcls = false,
                    RestrictPublicBuckets = false
                }),
                ObjectOwnership = ObjectOwnership.OBJECT_WRITER
            });
            
            new CfnBucketPolicy(this, "HistoricalFxRatesBucketPolicy", new CfnBucketPolicyProps
            {
                Bucket = bucketName,
                PolicyDocument = new PolicyDocument(new PolicyDocumentProps
                {
                    Statements = new[]
                    {
                        new PolicyStatement(new PolicyStatementProps
                        {
                            Effect = Effect.ALLOW,
                            Principals = new[] { new StarPrincipal() },
                            Actions = new[] { "s3:GetObject" },
                            Resources = new[] { $"arn:aws:s3:::{bucketName}/*" }
                        })
                    } 
                })
            });
        }
    }
}
