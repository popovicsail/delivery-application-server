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

    public async Task<IEnumerable<FeedbackQuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await _unitOfWork.FeedbackQuestions.GetAllAsync();
        return _mapper.Map<IEnumerable<FeedbackQuestionDto>>(questions);
    }

    public async Task<IEnumerable<FeedbackResponseDto>> GetUserFeedbackAsync(Guid userId)
    {
        var responses = await _unitOfWork.FeedbackResponses.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<FeedbackResponseDto>>(responses);
    }

    public async Task SubmitFeedbackAsync(Guid userId, IEnumerable<FeedbackCreateRequestDto> feedbackDtos)
    {
        var questions = await _unitOfWork.FeedbackQuestions.GetAllAsync();
        var questionCount = questions.Count();

        if (feedbackDtos.Count() != questionCount)
        {
            throw new BadRequestException($"You must rate all {questionCount} questions.");
        }

        var existingResponses = await _unitOfWork.FeedbackResponses.GetByUserIdAsync(userId);

        if (existingResponses.Any())
        {
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

    public async Task<PagedResultDto<FeedbackResponseWithUserDto>> GetFilteredResponsesAsync(FeedbackFilterRequestDto filter)
    {
        var now = DateTime.UtcNow;

        DateTime from = filter.TimeRange switch
        {
            "LastWeek" => now.AddDays(-7),
            "LastMonth" => now.AddMonths(-1),
            "Last3Months" => now.AddMonths(-3),
            "LastYear" => now.AddYears(-1),
            _ => DateTime.MinValue
        };

        var responses = await _unitOfWork.FeedbackResponses.GetResponsesByPeriodAsync(filter.QuestionId, from, now);
        var query = responses.AsQueryable();

        // 🔍 Filtriranje
        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            var term = filter.SearchTerm.ToLower();
            query = query.Where(r =>
                (r.User.FirstName + " " + r.User.LastName).ToLower().Contains(term) ||
                (r.Comment != null && r.Comment.ToLower().Contains(term))
            );
        }

        // 📊 Mapiranje
        var mapped = query.Select(r => new FeedbackResponseWithUserDto
        {
            QuestionId = r.QuestionId,
            UserId = r.UserId,
            UserFullName = r.User.FirstName + " " + r.User.LastName,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt
        });

        // 🔽 Sortiranje
        if (!string.IsNullOrEmpty(filter.SortField))
        {
            mapped = (filter.SortField, filter.SortOrder?.ToUpper()) switch
            {
                ("Rating", "ASC") => mapped.OrderBy(r => r.Rating),
                ("Rating", "DESC") => mapped.OrderByDescending(r => r.Rating),
                ("UserName", "ASC") => mapped.OrderBy(r => r.UserFullName),
                ("UserName", "DESC") => mapped.OrderByDescending(r => r.UserFullName),
                ("Date", "ASC") => mapped.OrderBy(r => r.CreatedAt),
                ("Date", "DESC") => mapped.OrderByDescending(r => r.CreatedAt),
                _ => mapped.OrderByDescending(r => r.CreatedAt)
            };
        }

        // 📄 Paginacija
        var totalCount = mapped.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);

        // Validacija broja strane
        if (filter.PageNumber < 1)
            filter.PageNumber = 1;
        if (filter.PageNumber > totalPages && totalPages > 0)
            filter.PageNumber = totalPages;

        var pagedItems = mapped
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        // 📦 Vraćanje rezultata
        return new PagedResultDto<FeedbackResponseWithUserDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

}
