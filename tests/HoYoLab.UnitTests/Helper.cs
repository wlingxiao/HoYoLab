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
