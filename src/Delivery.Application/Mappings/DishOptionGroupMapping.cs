using AutoMapper;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Domain.Entities.DishEntities;

public class DishOptionGroupMapping : Profile
{
    public DishOptionGroupMapping()
    {
        CreateMap<DishOptionGroupCreateRequestDto, DishOptionGroup>();
        CreateMap<DishOptionGroupUpdateRequestDto, DishOptionGroup>();
        CreateMap<DishOptionGroup, DishOptionGroupResponseDto>();

        CreateMap<DishOptionCreateDto, DishOption>();
        CreateMap<DishOptionUpdateRequestDto, DishOption>();
        CreateMap<DishOption, DishOptionDto>();
    }
}
