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

public class GenshinExchangeCdkeyRequest(string uid, string cdkey, string region) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://sg-hk4e-api.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?uid={uid}&region={region}&lang=zh-tw&cdkey={cdkey}&game_biz=hk4e_global&sLangKey=en-us");
}
