using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateAsync(CreateOrderRequestDto request);
        Task<OrderResponseDto> GetOneAsync(Guid orderId);
        Task<IEnumerable<OrderResponseDto>> GetAllAsync();
        Task UpdateStatusAsync(Guid orderId, string newStatus);
    }
}
