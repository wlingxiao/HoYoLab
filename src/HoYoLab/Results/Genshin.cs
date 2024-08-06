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

public class GenshinUserGameRoleHolder
{
    public required List<GenshinUserGameRole> List { get; set; }
}

public class GenshinUserGameRole
{
    public required string GameBiz { get; set; }
    public required string Region { get; set; }
    public required string GameUid { get; set; }
    public required string Nickname { get; set; }
    public required int Level { get; set; }
    public bool IsChosen { get; set; }
    public required string RegionName { get; set; }
    public bool IsOfficial { get; set; }
}
