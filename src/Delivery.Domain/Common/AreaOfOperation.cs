namespace Delivery.Domain.Common
{
    public class AreaOfOperation
    {
        public Guid Id { get; set; }   

        public string City { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsWeatherGood { get; set; } = true;
    }
}
