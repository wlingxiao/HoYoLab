namespace HoYoLab.Results;

public interface IDailyInfo
{
    public bool IsSign { get; }
    public string Today { get; }
    public int TotalSignDay { get; }
}
