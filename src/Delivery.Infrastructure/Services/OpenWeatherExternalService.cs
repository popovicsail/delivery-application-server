using System.Net.Http.Json;
using Delivery.Application.Dtos.External.OpenWeatherAPI.Requests;
using Delivery.Application.Dtos.External.OpenWeatherAPI.Responses;
using Delivery.Application.Interfaces;
using Delivery.Application.Settings;
using Microsoft.Extensions.Options;

namespace Delivery.Infrastructure.Services
{
    public class OpenWeatherExternalService : IOpenWeatherExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherSettings _weatherSettings;

        public OpenWeatherExternalService(HttpClient httpClient, IOptions<OpenWeatherSettings> weatherSettings)
        {
            _httpClient = httpClient;
            _weatherSettings = weatherSettings.Value;
        }

        public async Task<CurrentWeatherResponseDto?> GetCurrentWeatherAsync(CurrentWeatherRequestDto request)
        {
            var url = $"{_weatherSettings.BaseUrl}weather?lat={request.Lat}&lon={request.Lon}&appid={_weatherSettings.ApiKey}&units=metric";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CurrentWeatherResponseDto>(url);

                return response;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Fetching time not successful: {ex.Message}");

                return null;
            }
        }
    }
}