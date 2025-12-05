using AutoMapper;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<CreateRatingRequestDto, Rating>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // dodaje se naknadno
            .ForMember(dest => dest.UserId, opt => opt.Ignore());  // dodaje se naknadno
    }
}
