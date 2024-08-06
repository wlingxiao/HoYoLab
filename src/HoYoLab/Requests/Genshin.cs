using HoYoLab.Results;
using HoYoLab.Types;

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

public class GenshinExchangeCdkeyRequest(string uid, string cdkey, Region region, Language lang) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://sg-hk4e-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?uid={uid}&region={region.ToGenshinRegion()}&lang={lang.ToCode()}&cdkey={cdkey}&game_biz=hk4e_global&sLangKey=en-us");
}

public class GenshinUserGameRoleRequest(Region region, Language lang) : IRequest<UserGameRoleList>
{
    public HttpMethod Method { get; } = HttpMethod.Get;

    public Uri RequestUri() =>
        new Uri(
            $"https://api-account-os.hoyoverse.com/account/binding/api/getUserGameRolesByCookieToken?lang={lang.ToCode()}&region={region.ToGenshinRegion()}&game_biz=hk4e_global&sLangKey=en-us");
}
