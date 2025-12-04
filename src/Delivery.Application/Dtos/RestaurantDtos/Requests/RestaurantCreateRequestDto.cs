using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Dtos.RestaurantDtos.Requests;

public class RestaurantCreateRequestDto
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

