namespace Delivery.Application.Dtos.RestaurantDtos.Requests;

public class RestaurantCreateRequestDto
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}

