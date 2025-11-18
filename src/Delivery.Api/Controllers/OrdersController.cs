using System.Security.Claims;
using System.Text.Json.Serialization;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.OrderEntities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        // 1️⃣ Kreiranje porudžbine sa stavkama
        [HttpPost("items")]
        public async Task<ActionResult<Guid>> CreateOrderItems([FromBody] OrderItemsCreateRequestDto request)
        {
            var orderId = await _orderService.CreateItemsAsync(request, User);
            return Ok(new { orderId });
        }

        // 2️⃣ Dopuna porudžbine sa adresom i vaučerom
        [HttpPut("{orderId}/details")]
        public async Task<IActionResult> UpdateOrderDetails(Guid orderId, [FromBody] OrderUpdateDetailsDto request)
        {
            return Ok(await _orderService.UpdateDetailsAsync(orderId, request));
        }



        // 3️⃣ Potvrda porudžbine
        [HttpPost("{orderId}/confirm")]
        public async Task<IActionResult> ConfirmOrder(Guid orderId)
        {
            await _orderService.ConfirmAsync(orderId);
            return NoContent();
        }

        // GET: api/orders/{orderId}
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {

            var result = await _orderService.GetOneAsync(orderId);
            return Ok(result);
        }

        [HttpGet("draft")]
        public async Task<IActionResult> GetMyDraftAsync()
        {
            var result = await _orderService.GetDraftByCustomerAsync(User);
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId:guid}")]
        public async Task<IActionResult> GetByRestaurant(Guid restaurantId)
        {
            var result = await _orderService.GetByRestaurantAsync(restaurantId);
            return Ok(result);
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("courier/{courierId:guid}")]
        public async Task<IActionResult> GetByCourier(Guid courierId, DateTime? from = null, DateTime? to = null, int page = 1, int pageSize = 10)
        {
            // konverzija u UTC ako nisu null
            if (from.HasValue && from.Value.Kind == DateTimeKind.Unspecified)
                from = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);

            if (to.HasValue && to.Value.Kind == DateTimeKind.Unspecified)
                to = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);

            var result = await _orderService.GetByCourierAsync(courierId, from, to, page, pageSize);
            return Ok(result);
        }


        [HttpGet("customer/{customerId:guid}/deliveries-history")]
        public async Task<IActionResult> GetByCustomer(Guid customerId, int page = 1, int pageSize = 10)
        {
            var result = await _orderService.GetByCustomerAsync(customerId, page, pageSize);

            return Ok(new
            {
                items = result.Items,
                totalCount = result.TotalCount
            });
         }

        // PUT: api/orders/{orderId}/status
        [HttpPut("{orderId:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, [FromBody] OrderStatusUpdateRequestDto request)
        {
            await _orderService.UpdateStatusAsync(orderId, request.NewStatus, request.PrepTime);
            return NoContent();
        }

        [HttpDelete("{orderId:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid orderId)
        {
            await _orderService.DeleteAsync(orderId);
            return NoContent();
        }

        [HttpDelete("items/{itemId:guid}")]
        public async Task<IActionResult> DeleteOrderItemAsync(Guid itemId)
        {
            await _orderService.DeleteItemAsync(itemId);
            return NoContent();
        }
    }
}
