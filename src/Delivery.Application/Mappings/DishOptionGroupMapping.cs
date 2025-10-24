using AutoMapper;
using Delivery.Application.Dtos.DishDtos;
using Delivery.Application.Dtos.DishDtos.Responses;

public class DishOptionGroupMapping : Profile
{
    public DishOptionGroupMapping()
    {
        CreateMap<DishOptionGroup, DishOptionGroupDto>().ReverseMap();
        CreateMap<DishOptionGroupCreateRequestDto, DishOptionGroup>();
        CreateMap<DishOptionGroupUpdateRequestDto, DishOptionGroup>();

        CreateMap<DishOptionCreateDto, DishOption>();
        CreateMap<DishOptionUpdateRequestDto, DishOption>();
        CreateMap<DishOption, DishOptionDto>();
    }
}
