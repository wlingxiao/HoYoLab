namespace HoYoLab.UnitTests;

using static HoYoLab.UnitTests.Helper;

public class HsrTests
{
    [Fact]
    public async Task Test_DailyInfo()
    {
        var client =
            CreateHttpClient(
                """ {"retcode":0,"message":"OK","data":{"total_sign_day":5,"today":"2024-08-05","is_sign":true,"is_sub":false,"region":"","sign_cnt_missed":10,"short_sign_day":10,"send_first":false}} """);
        var info = await client.HsrDailyInfoAsync();
        Assert.Equal(5, info.TotalSignDay);
        Assert.Equal("2024-08-05", info.Today);
        Assert.True(info.IsSign);
        Assert.False(info.IsSub);
        Assert.Equal("", info.Region);
        Assert.Equal(10, info.SignCntMissed);
        Assert.Equal(10, info.ShortSignDay);
        Assert.False(info.SendFirst);
    }
}
