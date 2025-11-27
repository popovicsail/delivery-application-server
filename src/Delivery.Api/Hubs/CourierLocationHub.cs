using Microsoft.AspNetCore.SignalR;
using Delivery.Application.Interfaces;
using Delivery.Application.Dtos.OrderDtos.Requests;

namespace Delivery.Api.Hubs
{
    public class CourierLocationHub : Hub
    {
        private readonly ICourierLocationService _locationService;
        private readonly IOrderService _orderService;

        public CourierLocationHub(ICourierLocationService locationService, IOrderService orderService)
        {
            _locationService = locationService;
            _orderService = orderService;

        }

        // Kurir šalje svoju lokaciju preko WebSocket-a
        public async Task SendLocation(Guid orderId, double lat, double lng)
        {

            var dto = new CourierLocationDto
            {
                Lat = lat,
                Lng = lng,
                Timestamp = DateTime.UtcNow
            };

            // 1️⃣ Upis u bazu
            await _locationService.UpdateAsync(orderId, dto);

            // 2️⃣ Emituj lokaciju svim klijentima u grupi
            await Clients.Group(orderId.ToString())
                .SendAsync("ReceiveLocation", dto);
        }

        // Kupac se priključuje grupi za svoj order
        public async Task JoinOrder(Guid orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, orderId.ToString());
        }

        public async Task GetCurrentLocation(Guid orderId)
        {
            var order = await _orderService.GetOneAsync(orderId);
            if (order?.CourierLocationLat != null && order?.CourierLocationLng != null)
            {
                var dto = new CourierLocationDto
                {
                    Lat = order.CourierLocationLat,
                    Lng = order.CourierLocationLng,
                    Timestamp = order.CourierLocationUpdatedAt ?? DateTime.UtcNow
                };

                await Clients.Caller.SendAsync("ReceiveLocation", dto);
            }
        }
    }
}
