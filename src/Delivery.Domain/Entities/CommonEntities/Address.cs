namespace Delivery.Domain.Entities.CommonEntities;

public class Address
{
    public Guid Id { get; set; }
    public string StreetAndNumber { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

}
