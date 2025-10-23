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
        CreateMap<DishCreateRequestDto, Dish>();

        CreateMap<DishUpdateRequestDto, Dish>();

        CreateMap<Dish, DishSummaryResponseDto>();

        CreateMap<Dish, DishDetailResponseDto>()
            .ForMember(dest => dest.DishOptionGroups, opt => opt.MapFrom(src => src.DishOptionGroups));

        CreateMap<DishOptionGroup, DishOptionGroupResponseDto>()
            .ForMember(dest => dest.DishOptions, opt => opt.MapFrom(src => src.DishOptions));

        CreateMap<DishOption, DishOptionResponseDto>();


    }
}
