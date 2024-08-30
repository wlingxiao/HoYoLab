using System.Text.Json;
using System.Text.Json.Serialization;
using HoYoLab.Results;
using HoYoLab.Types;

namespace HoYoLab;

public class HoYoLabClientOptions(string? cookie)
{
    public string? Cookie { get; } = cookie;
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

    public bool IsNotLoggedIn => Retcode is -100;
    public bool IsAlreadyCheckedIn => Retcode is -5003;
    public bool IsInCooldown => Retcode is -2016;
}

[JsonSerializable(typeof(HoYoLabResult))]
[JsonSerializable(typeof(HoYoLabResult<GenshinDailyInfo>))]
[JsonSerializable(typeof(HoYoLabResult<HsrDailyInfo>))]
[JsonSerializable(typeof(HoYoLabResult<ZzzDailyInfo>))]
[JsonSerializable(typeof(HoYoLabResult<UserGameRoleList>))]
public partial class JsonContext : JsonSerializerContext
{
    public static readonly JsonContext DefaultContext = new(new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    });
}
