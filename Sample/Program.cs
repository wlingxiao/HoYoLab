using HoYoLab;

using var httpClient = new HttpClient();
var options = new HoYoLabClientOptions("cookie");
var client = new HoYoLabClient(options, httpClient);

var info = await client.GenshinDailyInfoAsync();
Console.WriteLine(info);
