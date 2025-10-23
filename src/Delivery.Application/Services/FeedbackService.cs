using AutoMapper;
using Delivery.Application.Dtos.FeedbackDtos;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.FeedbackEntities;
using Delivery.Domain.Interfaces;
using Delivery.Application.Exceptions;

namespace Delivery.Application.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // ✅ 1. Vraća sva pitanja za feedback
    public async Task<IEnumerable<FeedbackQuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await _unitOfWork.FeedbackQuestions.GetAllAsync();
        return _mapper.Map<IEnumerable<FeedbackQuestionDto>>(questions);
    }

    // ✅ 2. Vraća feedback korisnika (ako ga ima)
    public async Task<IEnumerable<FeedbackResponseDto>> GetUserFeedbackAsync(Guid userId)
    {
        var responses = await _unitOfWork.FeedbackResponses.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<FeedbackResponseDto>>(responses);
    }

    // ✅ 3. Kreira ili ažurira feedback korisnika
    public async Task SubmitFeedbackAsync(Guid userId, IEnumerable<FeedbackResponseDto> feedbackDtos)
    {
        var questions = await _unitOfWork.FeedbackQuestions.GetAllAsync();
        var questionCount = questions.Count();

        // ❌ Mora oceniti sva pitanja
        if (feedbackDtos.Count() != questionCount)
        {
            throw new BadRequestException($"You must rate all {questionCount} questions.");
        }

        // ⚙️ Proveri da li korisnik već ima feedback
        var existingResponses = await _unitOfWork.FeedbackResponses.GetByUserIdAsync(userId);

        if (existingResponses.Any())
        {
            // ✅ Ažuriraj postojeće odgovore
            foreach (var responseDto in feedbackDtos)
            {
                var existing = existingResponses.FirstOrDefault(r => r.QuestionId == responseDto.QuestionId);
                if (existing != null)
                {
                    existing.Rating = responseDto.Rating;
                    existing.Comment = responseDto.Comment;
                }
                else
                {
                    // Ako se doda novo pitanje kasnije, napravi novi odgovor
                    var newResponse = new FeedbackResponse(responseDto.QuestionId, responseDto.Rating, responseDto.Comment)
                    {
                        UserId = userId
                    };
                    await _unitOfWork.FeedbackResponses.AddAsync(newResponse);
                }
            }
        }
        else
        {
            // ✅ Kreiraj nove odgovore
            var responses = feedbackDtos.Select(dto => new FeedbackResponse(dto.QuestionId, dto.Rating, dto.Comment)
            {
                UserId = userId
            });

            foreach (var response in responses)
            {
                await _unitOfWork.FeedbackResponses.AddAsync(response);
            }
        }

        await _unitOfWork.CompleteAsync();
    }

    // ✅ 4. Statistika prosečnih ocena po pitanjima
    public async Task<IEnumerable<FeedbackStatsDto>> GetStatisticsAsync()
    {
        var questions = await _unitOfWork.FeedbackQuestions.GetAllAsync();
        var responses = await _unitOfWork.FeedbackResponses.GetAllAsync();

        var stats = questions.Select(q =>
        {
            var qResponses = responses.Where(r => r.QuestionId == q.Id).ToList();
            return new FeedbackStatsDto
            {
                QuestionId = q.Id,
                QuestionText = q.Text,
                AverageRating = qResponses.Any() ? qResponses.Average(r => r.Rating) : 0,
                TotalResponses = qResponses.Count,
                DailyAverages = qResponses
                    .GroupBy(r => r.CreatedAt.Date)
                    .Select(g => new DailyAverageDto
                    {
                        Date = g.Key,
                        Average = g.Average(r => r.Rating)
                    })
            };
        });

        return stats;
    }
}
