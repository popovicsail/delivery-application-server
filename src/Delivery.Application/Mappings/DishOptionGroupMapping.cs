using AutoMapper;

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
