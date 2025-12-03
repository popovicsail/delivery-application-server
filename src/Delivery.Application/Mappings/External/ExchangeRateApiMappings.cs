using AutoMapper;
using Delivery.Application.Dtos.External.ExchangeRateApi.Responses;
using Delivery.Domain.Common;

namespace Delivery.Application.Mappings.External
{
    public class ExchangeRateApiMappings : Profile
    {
        public ExchangeRateApiMappings() 
        {
            CreateMap<ExchangeRate, SingleCurrencyExchangeRateResponseDto>().ReverseMap();

            CreateMap<CurrentExchangeRateResponseDto, ExchangeRate>()
                .ForMember(dest => dest.Rates, opt => opt.MapFrom(src => src.ConversionRates))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Parse(src.LastUpdateUtc).ToUniversalTime()))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}