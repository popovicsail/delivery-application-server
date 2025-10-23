using Delivery.Application.Dtos.FeedbackDtos;
using Delivery.Domain.Entities.FeedbackEntities;

namespace Delivery.Application.Mappings;

public static class FeedbackMapper
{
    public static FeedbackQuestionDto ToDto(this FeedbackQuestion question) =>
        new() { Id = question.Id, Text = question.Text };

    public static FeedbackResponse ToEntity(this FeedbackResponseDto dto) =>
        new(dto.QuestionId, dto.Rating, dto.Comment);
}
