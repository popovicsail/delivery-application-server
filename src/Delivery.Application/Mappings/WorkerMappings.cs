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

        CreateMap<WorkerUpdateRequestDto, Worker>();

        CreateMap<Worker, WorkerSummaryResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));

        CreateMap<Worker, WorkerDetailResponseDto>()
            .ForMember(dest => dest.FirstName,
            opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
            opt => opt.MapFrom(src => src.User.LastName));
    }
}
