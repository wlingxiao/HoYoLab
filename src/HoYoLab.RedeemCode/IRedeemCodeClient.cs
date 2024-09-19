namespace HoYoLab.RedeemCode;

public interface IRedeemCodeClient
{
    public Task<CodeData?> GetCodesAsync(GameType game, CancellationToken cancellationToken = default);
}

public record CodeData(
    LatestCodeData LatestCodes,
    LivestreamCodeData LivestreamCodes,
    string? Description);

public record LatestCodeData(List<RedeemCode> GlobalExclusive);

public record LivestreamCodeData(
    List<RedeemCode> GlobalExclusive,
    List<RedeemCode> CnExclusive);

public record RedeemCode(string Code, bool? Expired, string Description);
