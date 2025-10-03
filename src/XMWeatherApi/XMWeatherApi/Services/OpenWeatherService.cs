using System.Net.Http.Json;
using XMWeatherApi.Models;
using Microsoft.Extensions.Configuration;

namespace XMWeatherApi.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;

        public OpenWeatherService(IHttpClientFactory factory, IConfiguration config)
        {
            _factory = factory;
            _config = config;
        }

        public async Task<WeatherDto> GetWeatherForCityAsync(string cityName, string countryCode, CancellationToken ct = default)
        {

            var apiKey = _config["OpenWeather:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("OpenWeather API key is not configured.");
            var client = _factory.CreateClient("OpenWeather");
            // Example url: /data/2.5/weather?q=Jakarta,ID&appid=KEY&units=imperial
            var url = $"data/2.5/weather?q={Uri.EscapeDataString(cityName)},{countryCode}&appid ={apiKey}&units = imperial";
            var resp = await client.GetAsync(url, ct);
            if (!resp.IsSuccessStatusCode)
                throw new HttpRequestException($"OpenWeather returned {resp.StatusCode}");
            var payload = await resp.Content.ReadFromJsonAsync<OpenWeatherResponse>(cancellationToken: ct);
            if (payload == null) throw new Exception("Empty payload from OpenWeather");

            // Map Dto response
            var dto = new WeatherDto
            {
                //already null checking above
                City = payload.Name,
                Country = countryCode,
                UtcTime = DateTime.UtcNow,
                WindSpeedMph = payload.Wind?.Speed ?? 0,
                WindDirectionDeg = payload.Wind?.Deg ?? 0,
                VisibilityMeters = payload.Visibility ?? 0,
                Sky = payload.Weather?.FirstOrDefault()?.Main ?? "",
                TempF = payload.Main?.Temp ?? 0,
                TempC = ConvertFtoC(payload.Main?.Temp ?? 0),
                DewPointF = payload.Main?.Temp ?? 0,
                // OpenWeather current doesn't return dew point in simple API; this is an approximation placeholder
                DewPointC = ConvertFtoC(payload.Main?.Temp ?? 0),
                RelativeHumidityPercent = payload.Main?.Humidity ?? 0,
                PressureHpa = payload.Main?.Pressure ?? 0

            };
            return dto;
        }
        private static double ConvertFtoC(double f) => Math.Round((f - 32) * 5.0 / 9.0, 2);
        // Minimal types to parse the subset we need
        private class OpenWeatherResponse
        {
            public string? Name { get; set; }
            public List<WeatherItem>? Weather { get; set; }
            public MainInfo? Main { get; set; }
            public WindInfo? Wind { get; set; }
            public int? Visibility { get; set; }
        }
        private class WeatherItem { public string? Main { get; set; } }
        private class MainInfo
        {
            public double? Temp { get; set; }
            public double? Pressure { get; set; }
            public double? Humidity { get; set; }
        }

        private class WindInfo
        {
            public double? Speed { get; set; }
            public int? Deg { get; set; }
        }
    }
}
