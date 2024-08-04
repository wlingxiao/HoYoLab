namespace HoYoLab.Results;

public class HoyoLabResult
{
    public required int Retcode { get; set; }
    public required string Message { get; set; }
}

public class HoyoLabResult<TData> : HoyoLabResult
{
    public required TData Data { get; set; }
}
