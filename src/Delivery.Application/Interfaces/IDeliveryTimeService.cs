namespace Delivery.Application.Interfaces
{
    public interface IDeliveryTimeService
    {
        Task<int?> GetEstimatedDeliveryTimeMinutesAsync(double restaurantLat, double restaurantLon, double customerLat, double customerLon);
    }
}
