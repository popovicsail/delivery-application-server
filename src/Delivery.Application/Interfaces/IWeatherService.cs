namespace Delivery.Application.Interfaces
{
    public interface IWeatherService
    {
        public Task<bool> UpdateWeatherConditionsAsync();
    }
}
