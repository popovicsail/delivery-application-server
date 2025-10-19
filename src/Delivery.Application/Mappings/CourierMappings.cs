using AutoMapper;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings;

public class CourierMappings : Profile
{
    public CourierMappings()
    {
        CreateMap<CourierCreateRequestDto, Courier>();

        CreateMap<CourierUpdateRequestDto, Courier>();

        CreateMap<Courier, CourierSummaryResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));

        CreateMap<Courier, CourierDetailResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));
    }
}
