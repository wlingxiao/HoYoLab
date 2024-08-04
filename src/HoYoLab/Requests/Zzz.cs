namespace HoYoLab.Requests;

public class ZzzExchangeCdkeyRequest(string uid, string cdkey, string region) : IRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public Uri RequestUri() => new Uri(
        $"https://public-operation-nap.hoyoverse.com/common/apicdkey/api/webExchangeCdkey?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}&lang=ja&game_biz=nap_global&uid={uid}&region={region}&cdkey={cdkey}");
}
