using Amazon.CDK;

namespace CrownForecaster.Deployment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new App();

            app.Synth();
        }
    }
}
