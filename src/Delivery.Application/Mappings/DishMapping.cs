using AutoMapper;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Domain.Entities.DishEntities;


namespace Delivery.Application.Mappings;

public class DishMappings : Profile
{
    public DishMappings()
    {
        CreateMap<DishCreateRequestDto, Dish>();

        CreateMap<DishUpdateRequestDto, Dish>();

        CreateMap<Dish, DishSummaryResponseDto>();

        CreateMap<Dish, DishDetailResponseDto>();
    }
}
