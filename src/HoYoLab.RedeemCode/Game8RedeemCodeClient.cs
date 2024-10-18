using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.XPath;

namespace HoYoLab.RedeemCode;

public class Game8RedeemCodeClient(HttpClient httpClient) : IRedeemCodeClient
{
    private readonly HtmlParser _htmlParser = new();

    public Task<CodeData?> GetCodesAsync(GameType game, CancellationToken cancellationToken = default) => game switch
    {
        GameType.Genshin => GetGenshinCodes(cancellationToken),
        GameType.Hsr => GetHsrCodes(cancellationToken),
        GameType.Zzz => GetZzzCodes(cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(game))
    };

    private async Task<CodeData?> GetGenshinCodes(CancellationToken cancellationToken = default)
    {
        var pageUrl = await GetGenshinCodesPageUrl(cancellationToken);
        if (string.IsNullOrWhiteSpace(pageUrl)) return null;
        return await GetGenshinCodesFromCodesPage(pageUrl, cancellationToken);
    }

    private async Task<CodeData?> GetHsrCodes(CancellationToken cancellationToken = default)
    {
        var pageUrl = await GetHsrCodesPageUrl(cancellationToken);
        if (string.IsNullOrWhiteSpace(pageUrl)) return null;
        return await GetHsrCodesFromCodesPage(pageUrl, cancellationToken);
    }

    private async Task<CodeData?> GetZzzCodes(CancellationToken cancellationToken = default)
    {
        var pageUrl = await GetZzzCodesPageUrl(cancellationToken);
        if (string.IsNullOrWhiteSpace(pageUrl)) return null;
        return await GetZzzCodesFromCodesPage(pageUrl, cancellationToken);
    }

    internal async Task<CodeData> GetGenshinCodesFromCodesPage(
        string url,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsStreamAsync(cancellationToken);
        var doc = await _htmlParser.ParseDocumentAsync(content, cancellationToken);
        var latest = ParseCodes(doc,
            "//p[text()='Here is a list of the new Genshin Impact codes:']/following-sibling::ol[1]/li");
        var global = ParseCodes(doc, "//h4[text()='Global-Exclusive Codes']/following-sibling::ol[1]/li");
        var cn = ParseCodes(doc, "//h4[text()='CN-Exclusive Codes']/following-sibling::ol[1]/li");
        var description = ParsePageDescription(doc, "//h3[contains(text(), 'Latest Redeem Codes in')]");
        var lastUpdatedOn = ParseLastUpdatedOn(doc);
        return new CodeData(new LatestCodeData(latest), new LivestreamCodeData(global, cn), description, lastUpdatedOn);
    }

    internal async Task<CodeData> GetHsrCodesFromCodesPage(
        string url,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsStreamAsync(cancellationToken);
        var doc = await _htmlParser.ParseDocumentAsync(content, cancellationToken);
        var latest = ParseCodes(doc,
            "//h3[contains(text(), 'New Redeem Codes in Version')]/following-sibling::ul[1]/li");
        var global = ParseCodes(doc, "//h3[text()='EN Livestream Codes']/following-sibling::ol[1]/li");
        var description = ParsePageDescription(doc, "//h3[contains(text(), 'New Redeem Codes in Version')]");
        return new CodeData(new LatestCodeData(latest), new LivestreamCodeData(global, []), Description: description,
            LastUpdatedOn: ParseLastUpdatedOn(doc));
    }

    internal async Task<CodeData> GetZzzCodesFromCodesPage(
        string url,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsStreamAsync(cancellationToken);
        var doc = await _htmlParser.ParseDocumentAsync(content, cancellationToken);
        var latest = ParseCodes(doc, "//h3[contains(text(), 'All Codes for')]/following-sibling::ul[1]/li");
        var global = ParseCodes(doc, "//h3[contains(text(), 'Livestream Codes')]/following-sibling::ul[1]/li");
        var description = ParsePageDescription(doc, "//h1[contains(text(), 'All Redeem Codes in')]");
        return new CodeData(new LatestCodeData(latest), new LivestreamCodeData(global, []), Description: description,
            LastUpdatedOn: ParseLastUpdatedOn(doc));
    }

    internal Task<string?> GetGenshinCodesPageUrl(CancellationToken cancellationToken = default)
        => GetCodesPageUrl(
            "https://game8.co/games/Genshin-Impact",
            "//ul/li/a[text()='Codes']",
            cancellationToken);

    internal Task<string?> GetHsrCodesPageUrl(CancellationToken cancellationToken = default)
        => GetCodesPageUrl(
            "https://game8.co/games/Honkai-Star-Rail",
            "//ul/li/a[text()='Redeem Codes']",
            cancellationToken);

    internal Task<string?> GetZzzCodesPageUrl(CancellationToken cancellationToken = default)
        => GetCodesPageUrl(
            "https://game8.co/games/Zenless-Zone-Zero",
            "//ul/li/a[text()='Codes']",
            cancellationToken);

    private async Task<string?> GetCodesPageUrl(string homeUrl, string path, CancellationToken cancellationToken)
    {
        var url = new Uri(homeUrl);
        using var response = await httpClient.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsStreamAsync(cancellationToken);
        var doc = await _htmlParser.ParseDocumentAsync(content, cancellationToken);
        var codesA = doc.Body?.SelectSingleNode(path);

        if (codesA is not IElement ele) return null;
        var href = ele.GetAttribute("href");
        if (string.IsNullOrWhiteSpace(href)) return null;
        if (href.StartsWith("http")) return href;

        return url.Scheme + "://" + url.Host + href;
    }

    private static string? ParsePageDescription(IHtmlDocument html, string path) =>
        html.Body?.SelectSingleNode(path).TextContent.Trim();

    private static List<RedeemCode> ParseCodes(IHtmlDocument html, string path)
    {
        var codesNode = html.Body?.SelectNodes(path);
        if (codesNode is null) return [];

        List<RedeemCode> codes = [];
        foreach (var node in codesNode)
        {
            var code = node.ChildNodes.FirstOrDefault(x => x.NodeName == "A")?.TextContent.Trim();
            if (code is null) continue;
            var expiredText = node.ChildNodes.FirstOrDefault(x => x.NodeName == "SPAN")?.TextContent.Trim();
            bool? expired = null;
            if (expiredText == "(EXPIRED)")
            {
                expired = true;
            }

            codes.Add(new RedeemCode(code, expired, node.TextContent.Trim()));
        }

        return codes;
    }

    private static string? ParseLastUpdatedOn(IHtmlDocument doc)
    {
        var timeEle = doc.Body?.SelectSingleNode("//time[contains(text(), 'Last updated on:')]");
        return timeEle is not IElement ele ? null : ele.GetAttribute("datetime");
    }
}
