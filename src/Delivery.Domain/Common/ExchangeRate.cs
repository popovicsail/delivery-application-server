namespace Delivery.Domain.Common
{
    public class ExchangeRate
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }
        public string BaseCode { get; set; }

        public Dictionary<string, decimal> Rates { get; set; }
    }
}