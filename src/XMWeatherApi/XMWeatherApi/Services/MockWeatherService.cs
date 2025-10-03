using XMWeatherApi.Models;

namespace XMWeatherApi.Services
{
    public class MockWeatherService : IWeatherService
    {

        public Task<WeatherDto> GetWeatherForCityAsync(string cityName, string
        countryCode, CancellationToken ct = default)
        {
            // Mock values (temperatures in Fahrenheit)
            //set
            var tempF = 77.0;  
            var dewF = 60.0;
            var dto = new WeatherDto
            {
                City = cityName,
                Country = countryCode,
                UtcTime = DateTime.UtcNow,
                WindSpeedMph = 5.0,
                WindDirectionDeg = 180,
                VisibilityMeters = 10000,
                Sky = "Clear",
                TempF = tempF,
                TempC = ConvertFtoC(tempF),
                DewPointF = dewF,
                DewPointC = ConvertFtoC(dewF),
                RelativeHumidityPercent = 60.0,
                PressureHpa = 1012.0
            };
            return Task.FromResult(dto);
        }
        // convertFtoC method for unit tests
        public static double ConvertFtoC(double f) => Math.Round((f - 32) *
        5.0 / 9.0, 2);

    }
}
