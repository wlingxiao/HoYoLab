using HoYoLab.Results;
using HoYoLab.Types;

namespace HoYoLab.Requests;

public abstract class ZzzRequest : BaseRequest
{
    protected override void UpdateRequest(HttpRequestMessage request) => request.Headers.Add("x-rpc-signgame", "zzz");
}

public abstract class ZzzRequest<TResponse> : ZzzRequest, IRequest<TResponse>;

public class ZzzDailyInfoRequest : ZzzRequest<ZzzDailyInfo>
{
    protected override string RequestUri =>
        "https://sg-act-nap-api.hoyolab.com/event/luna/zzz/os/info?act_id=e202406031448091";
}

public class ZzzDailyCheckInRequest : ZzzRequest
{
    protected override HttpMethod Method => HttpMethod.Post;

    protected override string RequestUri =>
        "https://sg-act-nap-api.hoyolab.com/event/luna/zzz/os/sign?act_id=e202406031448091";
}

public class ZzzExchangeCdkeyRequest(string uid, string cdkey, Region region, Language lang) : ZzzRequest
{
    protected override string RequestUri =>
        $"https://public-operation-nap.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang={lang.ToCode()}&game_biz=nap_global&uid={uid}&region={region.ToZzzRegion()}&cdkey={cdkey}";
}

public class ZzzUserGameRoleRequest(Region region, Language lang) : ZzzRequest<UserGameRoleList>
{
    protected override string RequestUri =>
        $"https://api-account-os.hoyoverse.com/account/binding/api/getUserGameRolesOfRegionByCookieToken?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&game_biz=nap_global&region={region.ToZzzRegion()}&lang={lang.ToCode()}";
}
