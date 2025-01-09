using HoYoLab;

var cookie = Environment.GetEnvironmentVariable("HOYOLAB_COOKIE");
if (string.IsNullOrEmpty(cookie))
{
    throw new Exception("The HOYOLAB_COOKIE environment variable is missing");
}

using var httpClient = new HttpClient();
var options = new HoYoLabClientOptions(cookie);
var client = new HoYoLabClient(options, httpClient);

Console.WriteLine((await client.GenshinDailyInfoAsync()).Today);
Console.WriteLine((await client.HsrDailyInfoAsync()).Today);
Console.WriteLine((await client.ZzzDailyInfoAsync()).Today);
