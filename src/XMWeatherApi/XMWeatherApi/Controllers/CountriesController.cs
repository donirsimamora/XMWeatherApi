using Microsoft.AspNetCore.Mvc;
using XMWeatherApi.Data;
namespace XMWeatherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly SeedData _seed;
        public CountriesController(SeedData seed) => _seed = seed;
        [HttpGet]
        public IActionResult GetCountries() => Ok(_seed.Countries);
        [HttpGet("{countryCode}/cities")]
        public IActionResult GetCities(string countryCode)
        {
            if (!
            _seed.CitiesByCountry.TryGetValue(countryCode.ToUpperInvariant(), out var list))
                return NotFound();
            return Ok(list);
        }
    }

}
