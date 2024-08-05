namespace HoYoLab.UnitTests;

using static HoYoLab.UnitTests.Helper;

public class ZzzTests
{
    [Fact]
    public async Task Test_DailyInfo()
    {
        var client =
            CreateHttpClient(
                """ {"retcode":0,"message":"OK","data":{"total_sign_day":0,"today":"2024-08-05","is_sign":false,"is_sub":true,"region":"","sign_cnt_missed":4,"short_sign_day":10,"send_first":true}} """);
        var info = await client.ZzzDailyInfoAsync();
        Assert.Equal(0, info.TotalSignDay);
        Assert.Equal("2024-08-05", info.Today);
        Assert.False(info.IsSign);
        Assert.True(info.IsSub);
        Assert.Equal("", info.Region);
        Assert.Equal(4, info.SignCntMissed);
        Assert.Equal(10, info.ShortSignDay);
        Assert.True(info.SendFirst);
    }
}
