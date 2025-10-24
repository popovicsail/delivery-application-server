using AutoMapper;
using Delivery.Application.Dtos.FeedbackDtos;
using Delivery.Domain.Entities.FeedbackEntities;

namespace Delivery.Application.Mappings;

public class FeedbackMappings : Profile
{
    public FeedbackMappings()
    {
        CreateMap<FeedbackQuestion, FeedbackQuestionDto>();

        CreateMap<FeedbackResponse, FeedbackResponseDto>();

    }
}
