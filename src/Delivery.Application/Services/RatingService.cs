using AutoMapper;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Interfaces;

public class RatingService : IRatingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> CreateRatingAsync(CreateRatingRequestDto dto, Guid userId)
    {
        string? imageBase64 = null;

        if (dto.Image is { Length: > 0 })
        {
            var allowedMimeTypes = new[] { "image/png", "image/jpeg" };
            var contentType = dto.Image.ContentType.ToLower();

            if (!allowedMimeTypes.Contains(contentType))
                throw new BadRequestException("Only PNG and JPEG images are allowed.");

            await using var ms = new MemoryStream();
            await dto.Image.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            imageBase64 = $"data:{contentType};base64,{Convert.ToBase64String(fileBytes)}";
        }

        var rating = _mapper.Map<Rating>(dto);
        rating.UserId = userId;
        rating.ImageUrl = imageBase64;

        await _unitOfWork.Rates.AddAsync(rating);
        await _unitOfWork.CompleteAsync();

        return rating.Id;
    }

    public async Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetRatingsForRestaurantAsync(
        Guid restaurantId,
        int page,
        int pageSize,
        DateTime? from = null,
        DateTime? to = null)
    {
        return await _unitOfWork.Rates.GetByTargetAsync(
            restaurantId,
            RatingTargetType.Restaurant,
            page,
            pageSize,
            from,
            to
        );
    }

    public async Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetRatingsForCourierAsync(
        Guid courierId,
        int page,
        int pageSize,
        DateTime? from = null,
        DateTime? to = null)
    {
        return await _unitOfWork.Rates.GetByTargetAsync(
            courierId,
            RatingTargetType.Courier,
            page,
            pageSize,
            from,
            to
        );
    }
}
