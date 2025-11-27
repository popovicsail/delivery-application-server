using AutoMapper;
using Delivery.Application.Dtos.OfferDtos;
using Delivery.Application.Dtos.OfferDtos.Requests;
using Delivery.Application.Dtos.OfferDtos.Responses;
using Delivery.Domain.Entities.OfferEntities;

namespace Delivery.Application.Mappings
{
    public class OfferMappings : Profile
    {
        public OfferMappings()
        {
            CreateMap<OfferCreateRequestDto, Offer>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt.ToUniversalTime()));
            CreateMap<OfferUpdateRequestDto, Offer>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt.ToUniversalTime()));

            CreateMap<Offer, OfferDetailsResponseDto>();

            CreateMap<OfferDish, OfferDishDto>().ReverseMap();
        }
    }
}
