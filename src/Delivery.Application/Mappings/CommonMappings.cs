using AutoMapper;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Responses;
using Delivery.Application.Dtos.CommonDtos.BaseWordSchedDtos;
using Delivery.Domain.Entities.CommonEntities;


namespace Delivery.Application.Mappings;

public class CommonMappings : Profile
{
    public CommonMappings()
    {
        CreateMap<BaseWorkSched, BaseWorkSchedDto>().ReverseMap();

        CreateMap<AddressDto, Address>().ReverseMap();

        CreateMap<AllergenCreateRequestDto, Allergen>();

        CreateMap<AllergenUpdateRequestDto, Allergen>();

        CreateMap<Allergen, AllergenSummaryResponseDto>();

        CreateMap<Allergen, AllergenDetailResponseDto>();
    }
}
