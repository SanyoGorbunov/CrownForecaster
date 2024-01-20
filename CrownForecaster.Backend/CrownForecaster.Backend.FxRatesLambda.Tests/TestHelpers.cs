namespace CrownForecaster.Backend.FxRatesLambda.Tests
{
    internal static class TestHelpers
    {
        public static async Task<Stream> StubStreamWithText(string text)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteAsync(text);
            await streamWriter.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
