namespace Delivery.Api.Contracts.Customers
{
    public class AddressUpdateRequest
    {
        public string StreetAndNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
