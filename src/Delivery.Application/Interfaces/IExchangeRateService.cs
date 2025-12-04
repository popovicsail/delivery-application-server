using Delivery.Application.Dtos.External.ExchangeRateApi.Responses;

namespace Delivery.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task UpdateExchangeRateAsync();
        Task<SingleCurrencyExchangeRateResponseDto?> GetCurrencyExchangeRateAsync(string currency);
    }
}