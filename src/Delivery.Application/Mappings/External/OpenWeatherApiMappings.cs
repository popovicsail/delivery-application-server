using AutoMapper;
using Delivery.Application.Dtos.External.OpenWeatherAPI.Requests;
using Delivery.Domain.Common;

namespace Delivery.Application.Mappings.External
{
    public class OpenWeatherApiMappings : Profile
    {
        public OpenWeatherApiMappings()
        {
            CreateMap<AreaOfOperation, CurrentWeatherRequestDto>();
        }
    }
}