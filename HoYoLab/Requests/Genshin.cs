using HoYoLab.Results;
using HoYoLab.Types;

namespace HoYoLab.Requests;

public abstract class GenshinRequest : BaseRequest
{
    protected override void UpdateRequest(HttpRequestMessage request) =>
        request.Headers.Add("x-rpc-device_uuid", "49b63815-3cce-4116-8589-4faf8c6c58d1");
}

public abstract class GenshinRequest<TResponse> : GenshinRequest, IRequest<TResponse>;

public class GenshinDailyInfoRequest : GenshinRequest<GenshinDailyInfo>
{
    protected override string RequestUri =>
        "https://sg-hk4e-api.hoyolab.com/event/sol/info?act_id=e202102251931481";
}

public class GenshinDailyCheckInRequest : GenshinRequest
{
    protected override HttpMethod Method => HttpMethod.Post;
    protected override string RequestUri => "https://sg-hk4e-api.hoyolab.com/event/sol/sign?act_id=e202102251931481";
}

public class GenshinExchangeCdkeyRequest(string uid, string cdkey, Region region, Language lang) : GenshinRequest
{
    protected override string RequestUri =>
        $"https://sg-hk4e-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?uid={uid}&region={region.ToGenshinRegion()}&lang={lang.ToCode()}&cdkey={cdkey}&game_biz=hk4e_global&sLangKey=en-us";
}

public class GenshinUserGameRoleRequest(Region region, Language lang) : GenshinRequest<UserGameRoleList>
{
    protected override string RequestUri =>
        $"https://api-account-os.hoyoverse.com/account/binding/api/getUserGameRolesByCookieToken?lang={lang.ToCode()}&region={region.ToGenshinRegion()}&game_biz=hk4e_global&sLangKey=en-us";
}
