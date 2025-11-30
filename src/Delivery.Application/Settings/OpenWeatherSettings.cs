namespace Delivery.Application.Settings
{
    public class OpenWeatherSettings
    {
        public const string SectionName = "OpenWeather";
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
    }
}