namespace HoYoLab.UnitTests;

using static HoYoLab.UnitTests.Helper;

public class GenshinTests
{
    [Fact]
    public async Task Test_NotLoggedIn()
    {
        var client = CreateHttpClient(""" {"data":null,"message":"Not logged in","retcode":-100} """);
        var error = await Assert.ThrowsAsync<HoYoLabException>(async () => { await client.GenshinDailyInfoAsync(); });
        Assert.Equal(-100, error.Retcode);
        Assert.Equal("Not logged in", error.Retmessage);
    }

    [Fact]
    public async Task Test_GetGenshinDailyInfo()
    {
        var client =
            CreateHttpClient(
                """ {"retcode":0,"message":"OK","data":{"total_sign_day":2,"today":"2024-08-03","is_sign":false,"first_bind":false,"is_sub":true,"region":"os_asia","month_last_day":false}} """);
        var info = await client.GenshinDailyInfoAsync();
        Assert.Equal(2, info.TotalSignDay);
        Assert.Equal("2024-08-03", info.Today);
        Assert.False(info.IsSign);
        Assert.False(info.FirstBind);
        Assert.True(info.IsSub);
        Assert.Equal("os_asia", info.Region);
        Assert.False(info.MonthLastDay);
    }

    [Fact]
    public async Task Test_ExchangeGenshinCdkey()
    {
        var client = CreateHttpClient(""" {"retcode":0,"message":"OK","data":{"msg":"兌換成功"}} """);
        await client.GenshinExchangeCdkeyAsync("1", "2");
        Assert.True(true);
    }

    [Fact]
    public async Task Test_GenshinDailyCheckIn()
    {
        var client = CreateHttpClient(""" {"retcode":0,"message":"OK"} """);
        await client.GenshinDailyCheckInAsync();
        Assert.True(true);
    }
}
