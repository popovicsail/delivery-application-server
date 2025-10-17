using AutoMapper;
using Delivery.Application.Dtos.Users.CustomerDtos.Responses;
using Delivery.Application.Dtos.Users.CustomerDtos.Requests;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings;

public class CustomerMappings : Profile
{
    public CustomerMappings()
    {
        CreateMap<CustomerCreateRequestDto, Customer>();

        CreateMap<CustomerUpdateRequestDto, Customer>();

        CreateMap<Customer, CustomerSummaryResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));

        CreateMap<Customer, CustomerDetailResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));
    }
}
