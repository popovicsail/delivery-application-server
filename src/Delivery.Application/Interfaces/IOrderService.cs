using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Domain.Entities.OrderEntities.Enums;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Application.Interfaces
{
    public interface IOrderService
    {
        Task ConfirmAsync(Guid orderId);
        Task<Guid> CreateItemsAsync(OrderItemsCreateRequestDto request, ClaimsPrincipal User);
        Task UpdateDetailsAsync(Guid orderId, OrderUpdateDetailsDto request);
        Task<OrderResponseDto> GetOneAsync(Guid orderId);
        Task<IEnumerable<OrderResponseDto>> GetAllAsync();
        Task UpdateStatusAsync(Guid orderId, int newStatus, int eta);
        Task<IEnumerable<OrderResponseDto>> GetByRestaurantAsync(Guid restaurantId);
        Task DeleteItemAsync(Guid orderItemId);
        Task DeleteAsync(Guid orderId);
        Task AutoAssignOrdersAsync();

        Task<IEnumerable<OrderResponseDto>> GetByCourierAsync(Guid courierId);
        Task<OrderDraftResponseDto>? GetDraftByCustomerAsync(ClaimsPrincipal User);
        Task<RestaurantRevenueStatisticsDto> GetRestaurantRevenueStatisticsAsync(Guid restaurantId, DateTime from, DateTime to);
        Task<DishRevenueStatisticsResponse> GetDishRevenueStatisticsAsync(Guid restaurantId, Guid dishId, DateTime from, DateTime to);
        Task<CanceledOrdersStatisticsDto> GetCanceledOrdersStatisticsAsync(Guid restaurantId, DateTime from, DateTime to);
    }
}
