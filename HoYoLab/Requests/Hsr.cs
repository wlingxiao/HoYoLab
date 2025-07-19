using HoYoLab.Results;
using HoYoLab.Types;

namespace HoYoLab.Requests;

public abstract class HsrRequest : BaseRequest
{
    protected override void UpdateRequest(HttpRequestMessage request) => request.Headers.Add("x-rpc-signgame", "hkrpg");
}

public abstract class HsrRequest<TResponse> : HsrRequest, IRequest<TResponse>;

public class HsrDailyInfoRequest : HsrRequest<HsrDailyInfo>
{
    protected override string RequestUri =>
        "https://sg-public-api.hoyolab.com/event/luna/os/info?act_id=e202303301540311";
}

public class HsrDailyCheckInRequest : HsrRequest
{
    protected override HttpMethod Method => HttpMethod.Post;

    protected override string RequestUri =>
        "https://sg-public-api.hoyolab.com/event/luna/os/sign?act_id=e202303301540311";
}

public class HsrExchangeCdkeyRequest(string uid, string cdkey, Region region, Language lang) : HsrRequest
{
    protected override string RequestUri =>
        $"https://sg-hkrpg-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang={lang.ToCode()}&game_biz=hkrpg_global&uid={uid}&region={region.ToHsrRegion()}&cdkey={cdkey}";
}

public class HsrUserGameRoleRequest(Region region, Language lang) : HsrRequest<UserGameRoleList>
{
    protected override string RequestUri =>
        $"https://api-account-os.hoyoverse.com/account/binding/api/getUserGameRolesOfRegionByCookieToken?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&game_biz=hkrpg_global&region={region.ToHsrRegion()}&lang={lang.ToCode()}";
}
