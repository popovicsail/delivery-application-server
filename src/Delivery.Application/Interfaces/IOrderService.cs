using System.Security.Claims;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IOrderService
{
    //summary
    //HELLO
    //summary
    Task ConfirmAsync(Guid orderId);
    Task<Guid> CreateItemsAsync(OrderItemsCreateRequestDto request, ClaimsPrincipal User);
    Task UpdateDetailsAsync(Guid orderId, OrderUpdateDetailsDto request);
    Task<OrderResponseDto> GetOneAsync(Guid orderId);
    Task<IEnumerable<OrderResponseDto>> GetAllAsync();
    Task<byte[]?> UpdateStatusAsync(Guid orderId, int newStatus, int eta);
    Task<IEnumerable<OrderResponseDto>> GetByRestaurantAsync(Guid restaurantId);
    Task DeleteItemAsync(Guid orderItemId);
    Task DeleteAsync(Guid orderId);
    Task AutoAssignOrdersAsync();

    Task<IEnumerable<OrderResponseDto>> GetByCourierAsync(Guid courierId);
    Task<OrderDraftResponseDto>? GetDraftByCustomerAsync(ClaimsPrincipal User);

    Task<byte[]> GetOrderBillPdfAsync(Guid orderId);
}
