namespace HoYoLab.Types;

public class UserGameRoleList
{
    public required List<UserGameRole> List { get; set; }
}

public class UserGameRole
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
