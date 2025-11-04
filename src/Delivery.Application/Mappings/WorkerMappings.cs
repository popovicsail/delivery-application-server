using AutoMapper;
using Delivery.Application.Dtos.Users.WorkerDtos.Responses;
using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings;

public class WorkerMappings : Profile
{
    public WorkerMappings()
    {
        CreateMap<WorkerCreateRequestDto, Worker>();

        CreateMap<WorkerUpdateRequestDto, Worker>()
            .ForPath(dest => dest.User.FirstName,
            opt => opt.MapFrom(src => src.FirstName))
            .ForPath(dest => dest.User.LastName,
            opt => opt.MapFrom(src => src.LastName))
            .ForPath(dest => dest.User.UserName,
            opt => opt.MapFrom(src => src.UserName))
            .ForPath(dest => dest.User.PhoneNumber,
            opt => opt.MapFrom(src => src.PhoneNumber))
            .ForPath(dest => dest.User.Email,
            opt => opt.MapFrom(src => src.Email))
            .ForPath(dest => dest.User.ProfilePictureBase64,
            opt => opt.MapFrom(src => src.ProfilePictureBase64));

        CreateMap<Worker, WorkerSummaryResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserName,
            opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.PhoneNumber,
            opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Email,
            opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.ProfilePictureBase64,
            opt => opt.MapFrom(src => src.User.ProfilePictureBase64));

        CreateMap<Worker, WorkerDetailResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserName,
            opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.PhoneNumber,
            opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Email,
            opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.ProfilePictureBase64,
            opt => opt.MapFrom(src => src.User.ProfilePictureBase64));
    }
}
