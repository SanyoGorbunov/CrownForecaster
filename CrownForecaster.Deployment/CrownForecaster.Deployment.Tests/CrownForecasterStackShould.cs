using Amazon.CDK;
using Amazon.CDK.Assertions;

namespace CrownForecaster.Deployment.Tests
{
    public class CrownForecasterStackShould
    {
        [Fact]
        public void HaveAllResources()
        {
            var app = new App();

            var crownForecasterStack = new CrownForecasterStack(app, "CrownForecasterStack");

            var template = Template.FromStack(crownForecasterStack);

            template.HasResource("AWS::S3::Bucket", new Dictionary<string, object>
            {
                { "Properties", new Dictionary<string, object>
                {
                    { "BucketName", "crown-forecaster-historical-fx-rates"},
                    { "AccessControl", "PublicRead" },
                    { "PublicAccessBlockConfiguration", new Dictionary<string, object>
                    {
                        { "BlockPublicAcls", false },
                        { "BlockPublicPolicy", false },
                        { "IgnorePublicAcls", false },
                        { "RestrictPublicBuckets", false },
                    }}
                }
            } });
        }
    }
}