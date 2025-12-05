namespace Delivery.Application.Settings
{
    public class ExchangeRateSettings
    {
        public const string SectionName = "ExchangeRateSettings";
   
        public required string ApiKey { get; set; }
        public required string[] BaseCodes { get; set; }
        public required string BaseUrl { get; set; }
    }
}