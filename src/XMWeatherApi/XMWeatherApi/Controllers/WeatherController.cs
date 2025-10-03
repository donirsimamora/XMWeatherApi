using Microsoft.AspNetCore.Mvc;
using XMWeatherApi.Services;

namespace XMWeatherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weather;
        public WeatherController(IWeatherService weather) => _weather = weather;
        [HttpGet("{cityName}")]
        public async Task<IActionResult> Get(string cityName, [FromQuery]string countryCode = "")
        {
            try
            {
                //until succeed
                var result = await _weather.GetWeatherForCityAsync(cityName,
                countryCode);
                return Ok(result);
                
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new
                {
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
