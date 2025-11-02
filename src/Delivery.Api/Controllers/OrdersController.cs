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

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto request)
        {
            var result = await _orderService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { orderId = result.OrderId }, result);
        }

        // GET: api/orders/{orderId}
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            var result = await _orderService.GetOneAsync(orderId);
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
        public async Task<IActionResult> GetByCourier(Guid courierId)
        {
            var result = await _orderService.GetByCourierAsync(courierId);
            return Ok(result);
        }

        // PUT: api/orders/{orderId}/status
        [HttpPut("{orderId:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, [FromBody] UpdateOrderStatusRequestDto request)
        {
            await _orderService.UpdateStatusAsync(orderId, request.NewStatus, request.PrepTime);
            return NoContent();
        }


    }
}
