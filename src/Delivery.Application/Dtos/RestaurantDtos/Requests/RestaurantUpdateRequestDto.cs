using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.BaseWordSchedDtos;

namespace Delivery.Application.Dtos.RestaurantDtos.Requests;

public class RestaurantUpdateRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public AddressDto Address { get; set; }
    public BaseWorkSchedDto? BaseWorkSched { get; set; }
    public string PhoneNumber { get; set; }
    public Guid OwnerId { get; set; }
}

