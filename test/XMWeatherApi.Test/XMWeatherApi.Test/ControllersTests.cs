using Microsoft.AspNetCore.Mvc;
using XMWeatherApi.Controllers;
using XMWeatherApi.Models;
using XMWeatherApi.Services;
using Moq;
using Xunit;


namespace XMWeatherApi.Test
{
    public class ControllersTests
    {
        [Fact]
        public async Task WeatherController_Returns_Ok()
        {
            var mock = new Mock<IWeatherService>();
            mock.Setup(s => s.GetWeatherForCityAsync("Jakarta", "ID", default))
            .ReturnsAsync(new WeatherDto
            {
                City = "Jakarta",
                Country =
            "ID"
            });
            var ctrl = new WeatherController(mock.Object);
            var res = await ctrl.Get("Jakarta", countryCode: "ID");
            Assert.IsType<OkObjectResult>(res);
        }
        [Fact]
        public async Task WeatherController_Returns_502_When_HttpError()
        {
            var mock = new Mock<IWeatherService>();
            mock.Setup(s => s.GetWeatherForCityAsync("Boom", "XX", default))
            .ThrowsAsync(new HttpRequestException("Bad gateway from upstream"));
            var ctrl = new WeatherController(mock.Object);
            var res = await ctrl.Get("Boom", countryCode: "XX");
            var status = Assert.IsType<ObjectResult>(res);
            Assert.Equal(502, status.StatusCode);
        }
    }

}
