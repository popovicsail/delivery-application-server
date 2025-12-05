using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Delivery.Domain.Entities.OrderEntities.Enums;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Requests;

namespace Delivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICourierLocationService _locationService;

        public OrdersController(IOrderService orderService, ICourierLocationService locationService)
        {
            _orderService = orderService;
            _locationService = locationService;
        }

        [HttpGet("/customer")]
        public async Task<IActionResult> GetOneNotDraftAsync()
        {
            return Ok(await _orderService.GetOneNotDraftAsync(User));
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
        public async Task<IActionResult> GetByRestaurant(Guid restaurantId, DateTime? from = null, DateTime? to = null, int page = 1, int pageSize = 10)
        {
            // konverzija u UTC ako nisu null
            if (from.HasValue && from.Value.Kind == DateTimeKind.Unspecified)
                from = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);

            if (to.HasValue && to.Value.Kind == DateTimeKind.Unspecified)
                to = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);

            var result = await _orderService.GetByRestaurantAsync(restaurantId, from, to, page, pageSize);
            return Ok(new
            {
                items = result.Items,
                totalCount = result.TotalCount
            });
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("courier/{courierId:guid}")]
        public async Task<IActionResult> GetByCourier(Guid courierId, DateTime? from = null, DateTime? to = null, int page = 1, int pageSize = 5)
        {
            // konverzija u UTC ako nisu null
            if (from.HasValue && from.Value.Kind == DateTimeKind.Unspecified)
                from = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);

            if (to.HasValue && to.Value.Kind == DateTimeKind.Unspecified)
                to = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);

            var result = await _orderService.GetByCourierAsync(courierId, from, to, page, pageSize);
            return Ok(new
            {
                items = result.Items,
                totalCount = result.TotalCount
            });
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

        [HttpGet("restaurant/{restaurantId:guid}/revenue")]
        public async Task<IActionResult> GetRevenue(
        Guid restaurantId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
        {
            var stats = await _orderService.GetRestaurantRevenueStatisticsAsync(restaurantId, from, to);
            return Ok(stats);
        }

        [HttpGet("dishes/{dishId:guid}/revenue")]
        public async Task<IActionResult> GetDishRevenue(
        Guid dishId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
        {
            from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
            to = DateTime.SpecifyKind(to, DateTimeKind.Utc);

            var stats = await _orderService.GetDishRevenueStatisticsAsync(dishId, from, to);
            return Ok(stats);
        }

        [HttpGet("restaurant/{restaurantId:guid}/canceled")]
        public async Task<IActionResult> GetCanceledOrdersStatistics(
        Guid restaurantId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
        {
            var stats = await _orderService.GetCanceledOrdersStatisticsAsync(restaurantId, from, to);
            return Ok(stats);
        }
        [HttpGet("{orderId:guid}/location")]
        public async Task<ActionResult<CourierLocationDto>> GetLocation(Guid orderId)
        {
            var location = await _locationService.GetByOrderAsync(orderId);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost("{orderId:guid}/location")]
        public async Task<IActionResult> UpdateLocation(Guid orderId, [FromBody] CourierLocationDto dto)
        {
            await _locationService.UpdateAsync(orderId, dto);
            return NoContent();
        }


        [HttpGet("{orderId:guid}/get-bill-pdf")]
        public async Task<IActionResult> GetBillPdf(Guid orderId)
        {
            var bill = await _orderService.GetOrderBillPdfAsync(orderId);

            if (bill == null)
            {
                return NotFound(new { Message = "ERROR: No bill for this order. Try again later." });
            }

            return File(bill, "application/pdf", $"bill-{orderId}.pdf");
        }

        [HttpGet("get-report-pdf")]
        public async Task<IActionResult> GetReportPdf(Guid restaurantId)
        {
            var report = await _orderService.GetReportPdfAsync(restaurantId);
            if (report == null)
            {
                return NotFound(new { Message = "ERROR: No report for this restaurant. Try again later." });
            }
            return File(report, "application/pdf", $"report-{restaurantId}.pdf");
        }
    }
}