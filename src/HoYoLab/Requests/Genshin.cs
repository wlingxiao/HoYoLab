using HoYoLab.Results;

namespace HoYoLab.Requests;

public class GenshinDailyInfoRequest : IRequest<GenshinDailyInfo>
{
    public HttpMethod Method => HttpMethod.Get;
    public Uri RequestUri() => new Uri("https://sg-hk4e-api.hoyolab.com/event/sol/info?act_id=e202102251931481");
}

public class GenshinDailyCheckInRequest : IRequest
{
    public HttpMethod Method => HttpMethod.Post;
    public Uri RequestUri() => new Uri("https://sg-hk4e-api.hoyolab.com/event/sol/sign?act_id=e202102251931481");
}

public class GenshinExchangeCdkeyRequest(string uid, string cdkey, GenshinRegion region) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://sg-hk4e-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?uid={uid}&region={region.GetRegion()}&lang=zh-tw&cdkey={cdkey}&game_biz=hk4e_global&sLangKey=en-us");
}

public enum GenshinRegion : byte
{
    America,
    Europe,
    Asia,
    TwHkMo
}

public static class GenshinRegionExtensions
{
    public static string GetRegion(this GenshinRegion region) => region switch
    {
        GenshinRegion.America => "os_usa",
        GenshinRegion.Europe => "os_euro",
        GenshinRegion.Asia => "os_asia",
        GenshinRegion.TwHkMo => "os_cht",
        _ => throw new IndexOutOfRangeException(nameof(region))
    };
}
