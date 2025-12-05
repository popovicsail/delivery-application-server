using AutoMapper;
using Delivery.Application.Dtos.External.ExchangeRateApi.Responses;
using Delivery.Application.Interfaces;
using Delivery.Domain.Common;
using Delivery.Domain.Interfaces;

namespace Delivery.Application.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangeRateExternalService _exchangeRateExternalService;
        private readonly IMapper _mapper;

        public ExchangeRateService(IUnitOfWork unitOfWork, IExchangeRateExternalService exchangeRateExternalService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _exchangeRateExternalService = exchangeRateExternalService;
            _mapper = mapper;
        }
        public async Task<SingleCurrencyExchangeRateResponseDto?> GetCurrencyExchangeRateAsync(string currency)
        {
            var response = await _unitOfWork.ExchangeRates.GetByBaseCodeAsync(currency);

            if (response == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<SingleCurrencyExchangeRateResponseDto>(response);

            return responseDto;
        }


        public async Task UpdateExchangeRateAsync()
        {
            var responses = await _exchangeRateExternalService.GetCurrentExchangeRateAsync();

            if (responses == null || !responses.Any()) return;

            foreach (var dto in responses)
            {
                var existingRate = await _unitOfWork.ExchangeRates.GetByBaseCodeAsync(dto.BaseCode);

                if (existingRate != null)
                {
                    existingRate.Timestamp = DateTime.UtcNow;
                    existingRate.Rates = dto.ConversionRates;

                    _unitOfWork.ExchangeRates.Update(existingRate);
                }
                else
                {
                    var newRate = _mapper.Map<ExchangeRate>(dto);
                    await _unitOfWork.ExchangeRates.AddAsync(newRate);
                }
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}