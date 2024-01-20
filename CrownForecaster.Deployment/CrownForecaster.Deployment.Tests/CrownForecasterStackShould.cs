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
                    { "PublicAccessBlockConfiguration", new Dictionary<string, object>
                    {
                        { "BlockPublicAcls", false },
                        { "BlockPublicPolicy", false },
                        { "IgnorePublicAcls", false },
                        { "RestrictPublicBuckets", false },
                    }},
                    { "OwnershipControls", new Dictionary<string, object>
                    {
                        { "Rules", new [] {
                            new Dictionary<string, object>
                            {
                                { "ObjectOwnership", "ObjectWriter" }
                            }
                        } }
                    } }
                }
            } });

            template.HasResource("AWS::S3::BucketPolicy", new Dictionary<string, object>
            {
                { "Properties", new Dictionary<string, object> {
                    { "PolicyDocument", new Dictionary<string, object> {
                        { "Statement", new[] {
                            new Dictionary<string, object>
                            {
                                { "Effect", "Allow" },
                                { "Principal", "*" },
                                { "Action", "s3:GetObject" },
                                { "Resource", "arn:aws:s3:::crown-forecaster-historical-fx-rates/*" }
                            }
                        } }
                    } }
                } }
            });
        }
    }
}