using XMWeatherApi.Models;

namespace XMWeatherApi.Services
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetWeatherForCityAsync(string cityName, string countryCode, CancellationToken ct = default);

    }
}
