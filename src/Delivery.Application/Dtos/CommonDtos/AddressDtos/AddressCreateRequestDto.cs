namespace Delivery.Application.Dtos.CommonDtos.AddressDtos
{
    public class AddressCreateRequest
    {
        public string StreetAndNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
