using System.Text.Json;
using System.Text.Json.Serialization;
using HoYoLab.Results;

namespace HoYoLab;

public class HoYoLabClientOptions(string cookie)
{
    public string Cookie { get; } = cookie;
}

public class HoYoLabException : Exception
{
    public HoYoLabException(string message) : base(message)
    {
    }

    public HoYoLabException(int retcode, string retmessage) : base($"retcode: {retcode} message: {retmessage}")
    {
        Retcode = retcode;
        Retmessage = retmessage;
    }

    public int? Retcode { get; }
    public string? Retmessage { get; }
}

[JsonSerializable(typeof(HoYoLabResult))]
[JsonSerializable(typeof(HoYoLabResult<GenshinDailyInfo>))]
public partial class JsonContext : JsonSerializerContext
{
    public static readonly JsonContext DefaultContext = new(new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    });
}
