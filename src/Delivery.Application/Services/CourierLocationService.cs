using AutoMapper;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Interfaces;

namespace Delivery.Application.Services
{
    public class CourierLocationService : ICourierLocationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourierLocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CourierLocationDto?> GetByOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetOneAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");

            if (order.CourierLocationLat == null || order.CourierLocationLng == null)
                return null;

            return new CourierLocationDto
            {
                Lat = order.CourierLocationLat.Value,
                Lng = order.CourierLocationLng.Value,
                Timestamp = order.CourierLocationUpdatedAt ?? DateTime.UtcNow
            };
        }

        public async Task UpdateAsync(Guid orderId, CourierLocationDto dto)
        {
            var order = await _unitOfWork.Orders.GetOneAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");

            // 1️⃣ Upis u bazu
            order.CourierLocationLat = dto.Lat;
            order.CourierLocationLng = dto.Lng;
            order.CourierLocationUpdatedAt = dto.Timestamp;

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
        }
    }
}
