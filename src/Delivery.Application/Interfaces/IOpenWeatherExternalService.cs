using Delivery.Application.Dtos.External.OpenWeatherAPI.Requests;
using Delivery.Application.Dtos.External.OpenWeatherAPI.Responses;

namespace Delivery.Application.Interfaces
{
    public interface IOpenWeatherExternalService
    {
        Task<CurrentWeatherResponseDto?> GetCurrentWeatherAsync(CurrentWeatherRequestDto request);
    }
}