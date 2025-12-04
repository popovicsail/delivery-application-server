using System.Text.Json.Serialization;

namespace Delivery.Application.Dtos.External.ExchangeRateApi.Responses
{
    public class CurrentExchangeRateResponseDto
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("base_code")]
        public string BaseCode { get; set; }

        [JsonPropertyName("time_last_update_utc")]
        public string LastUpdateUtc { get; set; }

        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> ConversionRates { get; set; }
    }
}