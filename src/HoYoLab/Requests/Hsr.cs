using HoYoLab.Results;

namespace HoYoLab.Requests;

public class HsrDailyInfoRequest : IRequest<HsrDailyInfo>
{
    public HttpMethod Method => HttpMethod.Get;
    public Uri RequestUri() => new Uri("https://sg-public-api.hoyolab.com/event/luna/os/info?act_id=e202303301540311");
}

public class HsrDailyCheckInRequest : IRequest
{
    public HttpMethod Method => HttpMethod.Post;
    public Uri RequestUri() => new Uri("https://sg-public-api.hoyolab.com/event/luna/os/sign?act_id=e202303301540311");
}

public class HsrExchangeCdkeyRequest(string uid, string cdkey, HsrRegion region) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://sg-hkrpg-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang=ja&game_biz=hkrpg_global&uid={uid}&region={region.GetRegion()}&cdkey={cdkey}");
}

public enum HsrRegion : byte
{
    America,
    Europe,
    Asia,
    TwHkMo
}

public static class HsrRegionExtensions
{
    public static string GetRegion(this HsrRegion region) => region switch
    {
        HsrRegion.America => "prod_official_usa",
        HsrRegion.Europe => "prod_official_eur",
        HsrRegion.Asia => "prod_official_asia",
        HsrRegion.TwHkMo => "prod_official_cht",
        _ => throw new IndexOutOfRangeException(nameof(region))
    };
}
