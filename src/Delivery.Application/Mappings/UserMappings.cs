using AutoMapper;
using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<RegisterRequestDto, User>()
                .ForMember(
                dest => dest.PasswordHash,
                opt => opt.Ignore()
                );

        CreateMap<OwnerCreateRequestDto, User>();


    }
}
