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
        //Src                     //Dest
        CreateMap<RestaurantUpdateRequestDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.AddressId, opt => opt.Ignore())
            .ForPath(dest => dest.Address.Id, opt => opt.Ignore())
            .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
            .ForPath(dest => dest.Address.StreetAndNumber, opt => opt.MapFrom(src => src.Address.StreetAndNumber))
            .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
            .ForMember(dest => dest.BaseWorkSched, opt =>
            {
                opt.Condition(src => src.BaseWorkSched != null);
                opt.MapFrom(src => src.BaseWorkSched);

            });

        CreateMap<Restaurant, RestaurantSummaryResponseDto>();

        CreateMap<Restaurant, RestaurantDetailResponseDto>();
    }
}
