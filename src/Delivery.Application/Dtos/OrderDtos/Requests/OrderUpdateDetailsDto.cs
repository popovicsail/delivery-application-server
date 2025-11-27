namespace Delivery.Application.Dtos.OrderDtos.Requests;

public class OrderUpdateDetailsDto
{
    public Guid AddressId { get; set; }
    public Guid? VoucherId { get; set; }
    public string? Note { get; set; }
}
