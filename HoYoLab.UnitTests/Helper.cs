using System.IO.Compression;
using System.Net;

namespace HoYoLab.UnitTests;

internal static class Helper
{
    public static IHoYoLabClient CreateHttpClient(string content)
    {
        var httpClient = new HttpClient(new MockHttpMessageHandler(content));
        var options = new HoYoLabClientOptions("cookie");
        var client = new HoYoLabClient(options, httpClient);
        return client;
    }

    public static HttpClient CreateHttpClientFromGZipFile(string path)
    {
        using var fs = File.OpenRead(path);
        using var gz = new GZipStream(fs, CompressionMode.Decompress);
        using var reader = new StreamReader(gz);
        return new HttpClient(new MockHttpMessageHandler(reader.ReadToEnd()));
    }
}

internal class MockHttpMessageHandler(string content) : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(content)
        };
        return Task.FromResult(response);
    }
}
