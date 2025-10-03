using System.Threading;
using XMWeatherApi.Services;
using Xunit;
namespace XMWeatherApi.Test
{
    public class WeatherServiceTests
    {
        [Fact]
        public void ConvertFtoC_KnownValues()
        {
            // FtoC 32 F -> 0 C
            Assert.Equal(0.0, MockWeatherService.ConvertFtoC(32));
            //FtoC 212 F -> 100 C
            Assert.Equal(100.0, MockWeatherService.ConvertFtoC(212));
        }
        [Fact]
        public async Task MockService_Returns_Weather()
        {
            var svc = new MockWeatherService();
            var w = await svc.GetWeatherForCityAsync("Jakarta", "ID");
            Assert.Equal("Jakarta", w.City);
            Assert.Equal("ID", w.Country);
            Assert.InRange(w.TempF, -150, 200);
        }
    }
}
