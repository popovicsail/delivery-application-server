using AutoMapper;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings;

public class OwnerMappings : Profile
{
    public OwnerMappings()
    {
        CreateMap<OwnerCreateRequestDto, Owner>();

        CreateMap<OwnerUpdateRequestDto, Owner>();

        CreateMap<Owner, OwnerSummaryResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));

        CreateMap<Owner, OwnerDetailResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));
    }
}
