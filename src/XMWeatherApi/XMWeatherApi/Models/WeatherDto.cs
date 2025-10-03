namespace XMWeatherApi.Models
{
    public class WeatherDto
    {
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime UtcTime { get; set; }
        public double WindSpeedMph { get; set; }
        public int WindDirectionDeg { get; set; }
        public int VisibilityMeters { get; set; }
        public string Sky { get; set; } = string.Empty;
        // Temperatures
        public double TempF { get; set; }
        public double TempC { get; set; }
        public double DewPointF { get; set; }
        public double DewPointC { get; set; }
        public double RelativeHumidityPercent { get; set; }
        public double PressureHpa { get; set; }

    }
}
