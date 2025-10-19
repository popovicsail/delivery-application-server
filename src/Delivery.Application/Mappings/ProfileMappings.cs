using AutoMapper;
using Delivery.Application.Dtos.Users.ProfileDtos.Responses;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings;

public class ProfileMappings : Profile
{
    public ProfileMappings()
    {
        CreateMap<ProfileResponseDto, User>().ReverseMap();

    }
}
