namespace HoYoLab.Results;

public class HsrDailyInfo : IDailyInfo
{
    public required string Today { get; set; }

    public int TotalSignDay { get; set; }

    public bool IsSign { get; set; }

    public bool IsSub { get; set; }

    public string? Region { get; set; }

    public int SignCntMissed { get; set; }

    public int ShortSignDay { get; set; }

    public bool SendFirst { get; set; }
}
