using Amazon.CDK;

namespace CrownForecaster.Deployment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new App();

            new CrownForecasterStack(app, "CrownForecasterStack");

            app.Synth();
        }
    }
}
