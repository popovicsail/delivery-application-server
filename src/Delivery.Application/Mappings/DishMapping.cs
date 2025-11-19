using AutoMapper;
using Delivery.Application.Dtos.DishDtos;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.RestaurantEntities;


namespace Delivery.Application.Mappings;

public class DishMappings : Profile
{
    public DishMappings()
    {
        CreateMap<Dish, DishDto>().ReverseMap();
        CreateMap<DishCreateRequestDto, Dish>()
            .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => 
                src.DiscountAmount > 0 ? src.DiscountAmount / 100 : 0))
            .ForMember(dest => dest.DiscountExpireAt, opt => opt.MapFrom(src => (src.DiscountExpireAt != null
            && (src.DiscountExpireAt > DateTime.Now) && src.DiscountAmount > 0) 
            ? src.DiscountExpireAt.Value.ToUniversalTime() : (DateTime?)null));

        CreateMap<DishUpdateRequestDto, Dish>()
            .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src =>
                src.DiscountAmount > 0 ? src.DiscountAmount / 100 : 0))
            .ForMember(dest => dest.DiscountExpireAt, opt => opt.MapFrom(src => (src.DiscountExpireAt != null
            && (src.DiscountExpireAt > DateTime.Now) && src.DiscountAmount > 0)
            ? src.DiscountExpireAt.Value.ToUniversalTime() : (DateTime?)null));

        CreateMap<Dish, DishSummaryResponseDto>();

        CreateMap<Dish, DishDetailResponseDto>()
            .ForMember(dest => dest.DishOptionGroups, opt => opt.MapFrom(src => src.DishOptionGroups));

        CreateMap<DishOptionGroup, DishOptionGroupResponseDto>()
            .ForMember(dest => dest.DishOptions, opt => opt.MapFrom(src => src.DishOptions));

        CreateMap<DishOption, DishOptionResponseDto>();


    }
}
