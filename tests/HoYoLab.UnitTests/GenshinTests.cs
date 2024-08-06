using HoYoLab.Types;

namespace HoYoLab.UnitTests;

using static HoYoLab.UnitTests.Helper;

public class GenshinTests
{
    [Fact]
    public async Task Test_NotLoggedIn()
    {
        var client = CreateHttpClient(""" {"data":null,"message":"Not logged in","retcode":-100} """);
        var error = await Assert.ThrowsAsync<HoYoLabException>(async () => { await client.GenshinDailyInfoAsync(); });
        Assert.True(error.IsNotLoggedIn);
        Assert.Equal("Not logged in", error.Retmessage);
    }

    [Fact]
    public async Task Test_AlreadyCheckedIn()
    {
        var client = CreateHttpClient(""" {"data":null,"message":"You've already checked in","retcode":-5003} """);
        var error = await Assert.ThrowsAsync<HoYoLabException>(async () => { await client.GenshinDailyInfoAsync(); });
        Assert.True(error.IsAlreadyCheckedIn);
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

    [Fact]
    public async Task Test_UserGameRoles()
    {
        var client =
            CreateHttpClient(
                """ {"retcode":0,"message":"OK","data":{"list":[{"game_biz":"hk4e_global","region":"os_asia","game_uid":"123","nickname":"🐇","level":10,"is_chosen":false,"region_name":"Asia Server","is_official":true}]}} """);
        var userGameRole = (await client.GenshinUserGameRolesAsync()).First();
        AssertUserGameRole(userGameRole);

        userGameRole = (await client.HsrUserGameRolesAsync()).First();
        AssertUserGameRole(userGameRole);

        userGameRole = (await client.ZzzUserGameRolesAsync()).First();
        AssertUserGameRole(userGameRole);
        return;

        static void AssertUserGameRole(UserGameRole userGameRole)
        {
            Assert.Equal("hk4e_global", userGameRole.GameBiz);
            Assert.Equal("os_asia", userGameRole.Region);
            Assert.Equal("123", userGameRole.GameUid);
            Assert.Equal("🐇", userGameRole.Nickname);
            Assert.Equal(10, userGameRole.Level);
            Assert.False(userGameRole.IsChosen);
            Assert.Equal("Asia Server", userGameRole.RegionName);
            Assert.True(userGameRole.IsOfficial);
        }
    }
}
