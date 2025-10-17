using AutoMapper;
using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Domain.Entities.RestaurantEntities;


namespace Delivery.Application.Mappings;

public class RestaurantMappings : Profile
{
    public RestaurantMappings()
    {
        CreateMap<RestaurantCreateRequestDto, Restaurant>();

        CreateMap<RestaurantUpdateRequestDto, Restaurant>();

        CreateMap<Restaurant, RestaurantSummaryResponseDto>();

        CreateMap<Restaurant, RestaurantDetailResponseDto>();
    }
}
