using AutoMapper;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Responses;
using Delivery.Application.Dtos.CommonDtos.BaseWordSchedDtos;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.RestaurantEntities;


namespace Delivery.Application.Mappings;

public class CommonMappings : Profile
{
    public CommonMappings()
    {
        CreateMap<BaseWorkSched, BaseWorkSchedDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<AddressCreateRequest, Address>();

        CreateMap<AddressUpdateRequest, Address>();

        CreateMap<AddressDto, Address>().ReverseMap();

        CreateMap<Allergen, AllergenDto>().ReverseMap();

        CreateMap<AllergenCreateRequestDto, Allergen>();

        CreateMap<AllergenUpdateRequestDto, Allergen>();

        CreateMap<Allergen, AllergenSummaryResponseDto>();

        CreateMap<Allergen, AllergenDetailResponseDto>();

        CreateMap<Menu, MenuDto>().ReverseMap();      
    }
}
