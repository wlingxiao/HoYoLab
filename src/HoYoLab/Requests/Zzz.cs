using HoYoLab.Results;

namespace HoYoLab.Requests;

public class ZzzDailyInfoRequest : IRequest<ZzzDailyInfo>
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() =>
        new Uri("https://sg-act-nap-api.hoyolab.com/event/luna/zzz/os/info?act_id=e202406031448091");
}

public class ZzzDailyCheckInRequest : IRequest
{
    public HttpMethod Method => HttpMethod.Post;

    public Uri RequestUri() =>
        new Uri("https://sg-act-nap-api.hoyolab.com/event/luna/zzz/os/sign?act_id=e202406031448091");
}

public class ZzzExchangeCdkeyRequest(string uid, string cdkey, ZzzRegion region) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://public-operation-nap.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang=ja&game_biz=nap_global&uid={uid}&region={region.GetRegion()}&cdkey={cdkey}");
}

public enum ZzzRegion : byte
{
    America,
    Europe,
    Asia,
    TwHkMo
}

public static class ZzzRegionExtensions
{
    public static string GetRegion(this ZzzRegion region) => region switch
    {
        ZzzRegion.America => "prod_gf_us",
        ZzzRegion.Europe => "prod_gf_eu",
        ZzzRegion.Asia => "prod_gf_jp",
        ZzzRegion.TwHkMo => "prod_gf_sg",
        _ => throw new IndexOutOfRangeException(nameof(region))
    };
}
