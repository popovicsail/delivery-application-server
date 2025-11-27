using Delivery.Application.Dtos.OrderDtos.Requests;

public interface ICourierLocationService
{
    Task<CourierLocationDto?> GetByOrderAsync(Guid orderId);
    Task UpdateAsync(Guid orderId, CourierLocationDto dto);
}
