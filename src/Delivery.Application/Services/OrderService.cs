using AutoMapper;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.OrderEntities.Enums;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Delivery.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetByRestaurantAsync(Guid restaurantId)
        {
            var orders = await _unitOfWork.Orders.GetByRestaurant(restaurantId);

            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetByCourierAsync(Guid courierId)
        {
            var orders = await _unitOfWork.Orders.GetByCourier(courierId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<Guid> CreateItemsAsync(CreateOrderItemsDto request)
        {
            // 1. Učitaj kupca
            var customer = await _unitOfWork.Customers.GetOneAsync(request.CustomerId)
                ?? throw new NotFoundException($"Customer with ID '{request.CustomerId}' not found.");

            // 2. Učitaj jela
            var dishIds = request.Items.Select(i => i.DishId).ToList();
            var dishes = await _unitOfWork.Dishes.GetByIdsWithAllergensAsync(dishIds);

            if (dishes.Count() != dishIds.Count)
                throw new NotFoundException("One or more dishes not found.");

            // 3. Validacija alergena
            var customerAllergens = customer.Allergens.Select(a => a.Name).ToHashSet();
            foreach (var dish in dishes)
            {
                if (dish.Allergens.Any(a => customerAllergens.Contains(a.Name)))
                    throw new BadRequestException($"Dish '{dish.Name}' contains allergens for this customer.");
            }

            // 4. Mapiraj Order iz DTO
            var order = _mapper.Map<Order>(request); // koristi CreateMap<CreateOrderItemsDto, Order>

            // 5. Mapiraj stavke i izračunaj cenu
            foreach (var itemDto in request.Items)
            {
                var dish = dishes.First(d => d.Id == itemDto.DishId);
                var item = _mapper.Map<OrderItem>(itemDto); // koristi CreateMap<OrderItemDto, OrderItem>
                item.Price = (decimal)(dish.Price * itemDto.Quantity);
                order.Items.Add(item);
            }
            order.CustomerId = customer.Id;
            order.TotalPrice = order.Items.Sum(i => i.Price);

            // 6. Sačuvaj porudžbinu
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return order.Id;
        }


        public async Task UpdateDetailsAsync(Guid orderId, UpdateOrderDetailsDto request)
        {
            var order = await _unitOfWork.Orders.GetOneWithCustomerAsync(orderId)
                ?? throw new NotFoundException($"Order with ID '{orderId}' not found.");

            var customer = order.Customer;

            if (!customer.Addresses.Any(a => a.Id == request.AddressId))
                throw new BadRequestException("Invalid delivery address for this customer.");

            order.AddressId = request.AddressId;

            if (request.VoucherId.HasValue)
            {
                var voucher = customer.Vouchers.FirstOrDefault(v =>
                    v.Id == request.VoucherId.Value && v.Status == "Active");

                if (voucher == null)
                    throw new BadRequestException("Selected voucher is invalid or inactive.");

                order.TotalPrice -= (decimal)voucher.DiscountAmount;
                if (order.TotalPrice < 0) order.TotalPrice = 0;

                voucher.Status = "Inactive";
                _unitOfWork.Vouchers.Update(voucher);
                order.VoucherId = voucher.Id;
            }

            await _unitOfWork.CompleteAsync();
        }



        public async Task ConfirmAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetOneAsync(orderId)
                ?? throw new NotFoundException($"Order with ID '{orderId}' not found.");

            if (order.Status != OrderStatus.Draft.ToString())
                throw new BadRequestException("Order is not in a confirmable state.");

            order.Status = OrderStatus.NaCekanju.ToString();
            order.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
        }

        public async Task<OrderResponseDto> GetOneAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetOneWithItemsAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllWithItemsAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task UpdateStatusAsync(Guid orderId, int newStatus ,int eta)
        {
            OrderStatus statusEnum = (OrderStatus)newStatus;

            var order = await _unitOfWork.Orders.GetOneWithItemsAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");

            if (eta > 0)
            {
                order.TimeToPrepare = eta;
            }

            order.Status = statusEnum.ToString();
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync(); // 👈 opet koristi tvoj metod
        }

        public async Task AutoAssignOrdersAsync()
        {
            // 1. Učitaj sve porudžbine koje čekaju preuzimanje i nemaju kurira
            var pendingOrders = (await _unitOfWork.Orders.GetAllAsync())
                .Where(o => o.Status == OrderStatus.CekaSePreuzimanje.ToString() && o.CourierId == null)
                .ToList();

            // 2. Učitaj sve kurire sa njihovim porudžbinama
            var couriers = await _unitOfWork.Couriers.GetAllWithOrdersAsync();

            foreach (var order in pendingOrders)
            {
                // 3. Nađi prvog slobodnog kurira sa manje od 2 aktivne dostave
                var courier = couriers.FirstOrDefault(c =>
                    c.WorkStatus == "AKTIVAN" &&
                    ((c.Orders?.Count(o => o.Status != OrderStatus.Zavrsena.ToString() &&
                                 o.Status != OrderStatus.Odbijena.ToString())) ?? 0) < 2);

                if (courier != null)
                {
                    // 4. Dodeli porudžbinu
                    order.CourierId = courier.Id;

                    _unitOfWork.Orders.Update(order);
                }
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
