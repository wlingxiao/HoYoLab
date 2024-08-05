using HoYoLab.Results;
using HoYoLab.Types;

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

public class ZzzExchangeCdkeyRequest(string uid, string cdkey, Region region, Language lang) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://public-operation-nap.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang={lang.ToCode()}&game_biz=nap_global&uid={uid}&region={region.ToZzzRegion()}&cdkey={cdkey}");
}
