using XMWeatherApi.Models;

namespace XMWeatherApi.Data
{

    public class SeedData
    {
        public List<Country> Countries { get; } = new()
        {
        new Country("US", "United States"),
        new Country("ID", "Indonesia"),
        new Country("GB", "United Kingdom")
        };
        public Dictionary<string, List<City>> CitiesByCountry { get; } = new()
        {
            ["US"] = new List<City>{ new City("New York","US"), new City("San Francisco","US"), new City("Chicago","US") },
            ["ID"] = new List<City>{ new City("Jakarta","ID"), new
            City("Bali","ID"), new City("Surabaya","ID") },
            ["GB"] = new List<City>{ new City("London","GB"), new
            City("Manchester","GB") }
};
        }
    
}
