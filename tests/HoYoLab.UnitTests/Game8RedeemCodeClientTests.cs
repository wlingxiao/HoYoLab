using HoYoLab.RedeemCode;

namespace HoYoLab.UnitTests;

public class Game8RedeemCodeClientTests
{
    [Fact]
    public async Task Test_GetGenshinCodesFromCodesPage()
    {
        var client = Helper.CreateHttpClientFromGZipFile("./Assets/Game8-Genshin-Codes-V5.0.html.gz");
        var codeClient = new Game8RedeemCodeClient(client);
        var codeData = await codeClient.GetGenshinCodesFromCodesPage(
            "https://game8.co/games/Genshin-Impact/archives/304759");

        Assert.Equal("Latest Redeem Codes in Version 5.0", codeData.Description);
        Assert.Equal("2024-09-14T00:57:35-04:00", codeData.LastUpdatedOn);

        Assert.Equal("DT4BZD5RT5U9", codeData.LatestCodes.GlobalExclusive[0].Code);
        Assert.Null(codeData.LatestCodes.GlobalExclusive[0].Expired);
        Assert.Equal("DT4BZD5RT5U9 - 60 Primogem and 5 Adventurer's Experience",
            codeData.LatestCodes.GlobalExclusive[0].Description);

        Assert.Equal("2SMTYV59TLFD", codeData.LivestreamCodes.GlobalExclusive[0].Code);
        Assert.True(codeData.LivestreamCodes.GlobalExclusive[0].Expired);
        Assert.Equal("2SMTYV59TLFD (EXPIRED)  - 100 Primogems and 10 Mystic Enhancement Ore",
            codeData.LivestreamCodes.GlobalExclusive[0].Description);

        Assert.Equal("XX3PD82ZYDGN", codeData.LivestreamCodes.CnExclusive[0].Code);
        Assert.True(codeData.LivestreamCodes.CnExclusive[0].Expired);
        Assert.Equal("XX3PD82ZYDGN (EXPIRED) - 100 Primogems and 10 Mystic Enhancement Ore",
            codeData.LivestreamCodes.CnExclusive[0].Description);
    }

    [Fact]
    public async Task Test_GetHsrCodesFromCodesPage()
    {
        var client = Helper.CreateHttpClientFromGZipFile("./Assets/Game8-Hsr-Codes-V2.5.html.gz");
        var codeClient = new Game8RedeemCodeClient(client);
        var codeData = await codeClient.GetHsrCodesFromCodesPage(
            "https://game8.co/games/Honkai-Star-Rail/archives/410296");

        Assert.Equal("New Redeem Codes in Version 2.5", codeData.Description);
        Assert.Equal("2024-09-17T00:13:12-04:00", codeData.LastUpdatedOn);
        Assert.Equal("BKWHQ9B3G7T1M", codeData.LatestCodes.GlobalExclusive[0].Code);
        Assert.Null(codeData.LatestCodes.GlobalExclusive[0].Expired);
        Assert.Equal("BKWHQ9B3G7T1M (Traveler's Guide x3, Immortal's Delight x3, Credit x20,000)",
            codeData.LatestCodes.GlobalExclusive[0].Description);

        Assert.Equal("DB3FKWZ4NUG7", codeData.LivestreamCodes.GlobalExclusive[0].Code);
        Assert.Null(codeData.LivestreamCodes.GlobalExclusive[0].Expired);
        Assert.Equal("DB3FKWZ4NUG7 - 100x Stellar Jades and 50,000x Credits",
            codeData.LivestreamCodes.GlobalExclusive[0].Description);
    }

    [Fact]
    public async Task Test_GetZzzCodesFromCodesPage()
    {
        var client = Helper.CreateHttpClientFromGZipFile("./Assets/Game8-Zzz-Codes-V1.2.html.gz");
        var codeClient = new Game8RedeemCodeClient(client);
        var codeData = await codeClient.GetZzzCodesFromCodesPage(
            "https://game8.co/games/Zenless-Zone-Zero/archives/435683");

        Assert.Equal("All Redeem Codes in September 2024", codeData.Description);
        Assert.Equal("2024-09-18T22:32:30-04:00", codeData.LastUpdatedOn);
        Assert.Equal("ZENLESSGIFT", codeData.LatestCodes.GlobalExclusive[0].Code);
        Assert.Null(codeData.LatestCodes.GlobalExclusive[0].Expired);
        Assert.Equal(
            "ZENLESSGIFT - Polychrome x50, Official Investigator Log x2, W-Engine Power Supply x3, Bangboo Algorithm Module x1",
            codeData.LatestCodes.GlobalExclusive[0].Description);

        Assert.Equal("TOURDEINFERNO", codeData.LivestreamCodes.GlobalExclusive[0].Code);
        Assert.Null(codeData.LivestreamCodes.GlobalExclusive[0].Expired);
        Assert.Equal(
            "TOURDEINFERNO - Polychrome x300,Senior Investigator Log x2, W-Engine Energy Module x3, Denny x30,000",
            codeData.LivestreamCodes.GlobalExclusive[0].Description);
    }

    [Fact]
    public async Task Test_GetCodesFromCodesPage()
    {
        var client = Helper.CreateHttpClientFromGZipFile("./Assets/Game8-Genshin-V5.0.html.gz");
        var codeClient = new Game8RedeemCodeClient(client);
        var url = await codeClient.GetGenshinCodesPageUrl();

        Assert.Equal("https://game8.co/games/Genshin-Impact/archives/304759", url);
    }

    [Fact]
    public async Task Test_GetHsrCodesPageUrl()
    {
        var client = Helper.CreateHttpClientFromGZipFile("./Assets/Game8-Hsr-V2.5.html.gz");
        var codeClient = new Game8RedeemCodeClient(client);
        var url = await codeClient.GetHsrCodesPageUrl();

        Assert.Equal("https://game8.co/games/Honkai-Star-Rail/archives/410296", url);
    }

    [Fact]
    public async Task Test_GetZzzCodesPageUrl()
    {
        var client = Helper.CreateHttpClientFromGZipFile("./Assets/Game8-Zzz-V1.2.html.gz");
        var codeClient = new Game8RedeemCodeClient(client);
        var url = await codeClient.GetZzzCodesPageUrl();

        Assert.Equal("https://game8.co/games/Zenless-Zone-Zero/archives/435683", url);
    }
}
