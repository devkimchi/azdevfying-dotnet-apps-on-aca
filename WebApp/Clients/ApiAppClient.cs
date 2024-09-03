using WebApp.Models;

namespace WebApp.Clients;

public interface IApiAppClient
{
    Task<List<WeatherForecast>> GetWeatherForecast();
}

public class ApiAppClient(HttpClient http) : IApiAppClient
{
    private readonly HttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<List<WeatherForecast>> GetWeatherForecast()
    {
        var result = await this._http.GetFromJsonAsync<List<WeatherForecast>>("weatherforecast");

        return result ?? [];
    }
}
