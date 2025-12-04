using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.BaseWordSchedDtos;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;

namespace Delivery.Application.Dtos.RestaurantDtos.Responses;

public class RestaurantDetailResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }

    public string Image { get; set; }
    public bool IsSuspended { get; set; }

    public AddressDto Address { get; set; }
    public OwnerDetailResponseDto Owner { get; set; }
    public BaseWorkSchedDto BaseWorkSched { get; set; }
    public List<MenuDto> Menus { get; set; }
}

