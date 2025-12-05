using System.Net.Http.Json;
using Delivery.Application.Dtos.External.ExchangeRateApi.Responses;
using Delivery.Application.Interfaces;
using Delivery.Application.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Delivery.Infrastructure.Services
{
    public class ExchangeRateExternalService : IExchangeRateExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly ExchangeRateSettings _settings;

        public ExchangeRateExternalService(HttpClient httpClient, IOptions<ExchangeRateSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<CurrentExchangeRateResponseDto>?> GetCurrentExchangeRateAsync()
        {
            var responses = new List<CurrentExchangeRateResponseDto>();

            if (_settings.BaseCodes == null || !_settings.BaseCodes.Any())
            {
                return null;
            }

            foreach (var code in _settings.BaseCodes)
            {
                var url = $"{_settings.BaseUrl}{_settings.ApiKey}/latest/{code}";

                try
                {
                    var response = await _httpClient.GetFromJsonAsync<CurrentExchangeRateResponseDto>(url);

                    if (response != null)
                    {
                        responses.Add(response);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Fetching exchange rate for {code} failed: {ex.Message}");
                }
            }

            return responses.Any() ? responses : null;
        }
    }
}