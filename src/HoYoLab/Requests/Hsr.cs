using HoYoLab.Results;
using HoYoLab.Types;

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

public class HsrExchangeCdkeyRequest(string uid, string cdkey, Region region, Language lang) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://sg-hkrpg-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang={lang.ToCode()}&game_biz=hkrpg_global&uid={uid}&region={region.ToHsrRegion()}&cdkey={cdkey}");
}

public class HsrUserGameRoleRequest(Region region, Language lang) : IRequest<UserGameRoleList>
{
    public HttpMethod Method { get; } = HttpMethod.Get;

    public Uri RequestUri() =>
        new Uri(
            $"https://api-account-os.hoyoverse.com/account/binding/api/getUserGameRolesOfRegionByCookieToken?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&game_biz=hkrpg_global&region={region.ToHsrRegion()}&lang={lang.ToCode()}");
}
