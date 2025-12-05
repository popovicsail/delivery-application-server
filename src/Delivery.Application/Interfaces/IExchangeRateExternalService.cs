using Delivery.Application.Dtos.External.ExchangeRateApi.Responses;

namespace Delivery.Application.Interfaces
{
    public interface IExchangeRateExternalService
    {
        Task<IEnumerable<CurrentExchangeRateResponseDto>?> GetCurrentExchangeRateAsync();
    }
}