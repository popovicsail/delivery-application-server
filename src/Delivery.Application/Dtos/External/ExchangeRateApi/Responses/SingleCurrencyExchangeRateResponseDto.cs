namespace Delivery.Application.Dtos.External.ExchangeRateApi.Responses
{
    public class SingleCurrencyExchangeRateResponseDto
    {
        public DateTime Timestamp { get; set; }
        public required string BaseCode { get; set; }

        public required Dictionary<string, decimal> Rates { get; set; }
    }
}