namespace HoYoLab.Results;

public class GenshinDailyInfo
{
    public required string Today { get; set; }

    public int TotalSignDay { get; set; }

    public bool IsSign { get; set; }

    public bool FirstBind { get; set; }

    public bool IsSub { get; set; }

    public required string Region { get; set; }

    public bool MonthLastDay { get; set; }
}
