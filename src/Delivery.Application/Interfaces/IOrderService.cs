using System.Security.Claims;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> GetOneNotDraftAsync(ClaimsPrincipal User);
        Task ConfirmAsync(Guid orderId);
        Task<Guid> CreateItemsAsync(OrderItemsCreateRequestDto request, ClaimsPrincipal User);
        Task<OrderResponseDto> UpdateDetailsAsync(Guid orderId, OrderUpdateDetailsDto request);
        Task<OrderResponseDto> GetOneAsync(Guid orderId);
        Task<IEnumerable<OrderResponseDto>> GetAllAsync();
        Task UpdateStatusAsync(Guid orderId, int newStatus, int eta);
        Task<IEnumerable<OrderResponseDto>> GetByRestaurantAsync(Guid restaurantId);
        Task DeleteItemAsync(Guid orderItemId);
        Task DeleteAsync(Guid orderId);
        Task AutoAssignOrdersAsync();

        Task<(IEnumerable<OrderResponseDto> Items, int TotalCount)> GetByCourierAsync(
         Guid courierId,
         DateTime? from = null,
         DateTime? to = null,
         int page = 1,
         int pageSize = 10);
        Task<(IEnumerable<OrderResponseDto> Items, int TotalCount)> GetByCustomerAsync(
        Guid customerId,
        int page = 1,
        int pageSize = 10);
        Task<OrderDraftResponseDto>? GetDraftByCustomerAsync(ClaimsPrincipal User);

    Task<byte[]> GetOrderBillPdfAsync(Guid orderId);
}
