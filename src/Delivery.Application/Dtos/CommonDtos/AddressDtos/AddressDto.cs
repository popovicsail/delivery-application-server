namespace Delivery.Application.Dtos.CommonDtos.AddressDtos;

public class AddressDto
{
    public Guid? Id { get; set; }
    public string StreetAndNumber { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
}
