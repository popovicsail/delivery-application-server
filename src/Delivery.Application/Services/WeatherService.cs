using AutoMapper;
using Delivery.Application.Dtos.External.OpenWeatherAPI.Requests;
using Delivery.Application.Interfaces;
using Delivery.Domain.Interfaces;

namespace Delivery.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly IOpenWeatherExternalService _openWeatherExternalService;

        public WeatherService (IUnitOfWork unitOfWork, IMapper autoMapper, IOpenWeatherExternalService openWeatherExternalService)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _openWeatherExternalService = openWeatherExternalService;
        }

        public async Task<bool> UpdateWeatherConditionsAsync()
        {
            var areasOfOperation = await _unitOfWork.AreasOfOperation.GetAllAsync();

            bool hasChanges = false;

            foreach (var area in areasOfOperation)
            {
                var request = _autoMapper.Map<CurrentWeatherRequestDto>(area);
                var response = await _openWeatherExternalService.GetCurrentWeatherAsync(request);
                if (response == null)
                {
                    continue;
                }

                bool isWeatherGood = response.Main.Temp <= 40 && response.Main.Temp > 0;

                if (area.IsWeatherGood != isWeatherGood)
                {
                    area.IsWeatherGood = isWeatherGood;
                    _unitOfWork.AreasOfOperation.Update(area);
                    hasChanges = true;
                }
                await Task.Delay(200);
            }

            if (hasChanges)
            {
                await _unitOfWork.CompleteAsync();
            }
           
            return true;
        }
    }
}