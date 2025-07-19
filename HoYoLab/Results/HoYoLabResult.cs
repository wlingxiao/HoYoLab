namespace HoYoLab.Results;

public class HoYoLabResult
{
    public required int Retcode { get; set; }
    public required string Message { get; set; }
}

public class HoYoLabResult<TData> : HoYoLabResult
{
    public required TData Data { get; set; }
}
