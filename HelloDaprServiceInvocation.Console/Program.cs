// See https://aka.ms/new-console-template for more information

using Dapr.Client;

Console.WriteLine("Invoking Web API ...");

using var client = new DaprClientBuilder().Build();

var weatherForecasts =
    await client.InvokeMethodAsync<List<WeatherForecast>>(
        HttpMethod.Get, "api", "weatherforecast");

foreach (var weatherForecast in weatherForecasts)
{
    Console.WriteLine(
        $"Date:{weatherForecast.Date}, TemperatureC:{weatherForecast.TemperatureC}, Summary:{weatherForecast.Summary}");
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}