using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using HoYoLab.Requests;
using HoYoLab.Results;

namespace HoYoLab;

public interface IHoYoLabClient
{
    Task SendAsync(IRequest request, CancellationToken cancellationToken = default);

    Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        JsonTypeInfo<HoYoLabResult<TResponse>> jsonTypeInfo,
        CancellationToken cancellationToken = default);
}

public class HoYoLabClient : IHoYoLabClient
{
    private readonly HoYoLabClientOptions _options;
    private readonly HttpClient _httpClient;

    public HoYoLabClient(HoYoLabClientOptions options, HttpClient httpClient)
    {
        _options = options;
        _httpClient = httpClient;
    }

    public async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        using var httpResp = await _httpClient.SendAsync(CreateRequest(request), cancellationToken);
        CheckResult(await CheckResponse(httpResp)
            .Content
            .ReadFromJsonAsync(JsonContext.DefaultContext.HoYoLabResult, cancellationToken));
    }

    public async Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        JsonTypeInfo<HoYoLabResult<TResponse>> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        using var httpResp = await _httpClient.SendAsync(CreateRequest(request), cancellationToken);
        var result = await CheckResponse(httpResp)
            .Content
            .ReadFromJsonAsync(jsonTypeInfo, cancellationToken);
        return CheckResult(result).Data;
    }

    private HttpRequestMessage CreateRequest(IRequest request)
    {
        var httpReq = new HttpRequestMessage
        {
            RequestUri = request.RequestUri(),
            Method = request.Method
        };
        httpReq.Headers.Add("Cookie", _options.Cookie);
        return httpReq;
    }

    private static HttpResponseMessage CheckResponse(HttpResponseMessage response)
    {
        if (response.StatusCode is not HttpStatusCode.OK)
        {
            throw new HoYoLabException(
                $"Invalid status code {response.StatusCode}.");
        }

        return response;
    }

    private static TResult CheckResult<TResult>(TResult? result) where TResult : HoYoLabResult
    {
        if (result is null)
        {
            throw new HoYoLabException("Result is null.");
        }

        if (result.Retcode != 0)
        {
            throw new HoYoLabException(result.Retcode, result.Message);
        }

        return result;
    }
}

public static class HoYoLabClientExtensions
{
    #region Genshin

    public static async Task<GenshinDailyInfo> GenshinDailyInfoAsync(
        this IHoYoLabClient client,
        CancellationToken cancellationToken = default)
        => await client.SendAsync(
            new GenshinDailyInfoRequest(),
            JsonContext.DefaultContext.HoYoLabResultGenshinDailyInfo,
            cancellationToken);

    public static async Task GenshinDailyCheckInAsync(
        this IHoYoLabClient client,
        CancellationToken cancellationToken = default) =>
        await client.SendAsync(new GenshinDailyCheckInRequest(), cancellationToken);

    public static async Task GenshinExchangeCdkeyAsync(
        this IHoYoLabClient client,
        string uid,
        string cdkey,
        GenshinRegion region = GenshinRegion.Asia,
        CancellationToken cancellationToken = default) =>
        await client.SendAsync(new GenshinExchangeCdkeyRequest(uid, cdkey, region), cancellationToken);

    #endregion

    #region Zzz

    public static async Task<ZzzDailyInfo> ZzzDailyInfoAsync(
        this IHoYoLabClient client,
        CancellationToken cancellationToken = default)
        => await client.SendAsync(
            new ZzzDailyInfoRequest(),
            JsonContext.DefaultContext.HoYoLabResultZzzDailyInfo,
            cancellationToken);

    public static async Task ZzzDailyCheckInAsync(
        this IHoYoLabClient client,
        CancellationToken cancellationToken = default) =>
        await client.SendAsync(new ZzzDailyCheckInRequest(), cancellationToken);

    public static async Task ZzzExchangeCdkeyAsync(
        this IHoYoLabClient client,
        string uid,
        string cdkey,
        ZzzRegion region = ZzzRegion.Asia,
        CancellationToken cancellationToken = default) =>
        await client.SendAsync(new ZzzExchangeCdkeyRequest(uid, cdkey, region), cancellationToken);

    #endregion

    #region Hsr

    public static async Task<HsrDailyInfo> HsrDailyInfoAsync(
        this IHoYoLabClient client,
        CancellationToken cancellationToken = default)
        => await client.SendAsync(
            new HsrDailyInfoRequest(),
            JsonContext.DefaultContext.HoYoLabResultHsrDailyInfo,
            cancellationToken);

    public static async Task HsrDailyCheckInAsync(
        this IHoYoLabClient client,
        CancellationToken cancellationToken = default) =>
        await client.SendAsync(new HsrDailyCheckInRequest(), cancellationToken);

    public static async Task HsrExchangeCdkeyAsync(
        this IHoYoLabClient client,
        string uid,
        string cdkey,
        HsrRegion region = HsrRegion.Asia,
        CancellationToken cancellationToken = default) =>
        await client.SendAsync(new HsrExchangeCdkeyRequest(uid, cdkey, region), cancellationToken);

    #endregion
}
