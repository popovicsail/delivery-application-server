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

        public async Task<OrderResponseDto> CreateAsync(CreateOrderRequestDto request)
        {
            // 1. Učitaj kupca sa adresama i alergenima
            var customer = await _unitOfWork.Customers.GetOneAsync(request.CustomerId);

            if (customer == null)
                throw new NotFoundException($"Customer with ID '{request.CustomerId}' not found.");

            // 2. Validacija adrese
            if (!customer.Addresses.Any(a => a.Id == request.AddressId))
                throw new BadRequestException("Invalid delivery address for this customer.");

            // 3. Učitaj jela sa alergenima
            var dishIds = request.Items.Select(i => i.DishId).ToList();
            var dishes = await _unitOfWork.Dishes.GetByIdsWithAllergensAsync(dishIds);

            if (dishes.Count() != dishIds.Count)
                throw new NotFoundException("One or more dishes not found.");

            // 4. Validacija alergena
            var customerAllergens = customer.Allergens.Select(a => a.Name).ToHashSet();
            foreach (var dish in dishes)
            {
                if (dish.Allergens.Any(a => customerAllergens.Contains(a.Name)))
                    throw new BadRequestException($"Dish '{dish.Name}' contains allergens for this customer.");
            }

            // 5. Kreiraj Order
            var order = _mapper.Map<Order>(request);
            order.CreatedAt = DateTime.UtcNow;

            // 6. Dodaj stavke i izračunaj cenu
            foreach (var item in request.Items)
            {
                var dish = dishes.First(d => d.Id == item.DishId);

                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    DishId = dish.Id,
                    Quantity = item.Quantity,
                    Price = (decimal)(dish.Price * item.Quantity)
                });
            }

            order.TotalPrice = order.Items.Sum(i => i.Price);
            order.Customer = customer;
            order.Status = OrderStatus.NaCekanju.ToString();
            order.RestaurantId = request.RestaurantId;

            // 7. Primeni vaučer ako postoji
            Voucher? selectedVoucher = null;

            if (request.VoucherId.HasValue)
            {
                selectedVoucher = customer.Vouchers
                    .FirstOrDefault(v => v.Id == request.VoucherId.Value && v.Status == "Active");

                if (selectedVoucher == null)
                    throw new BadRequestException("Selected voucher is invalid or inactive.");
            }

            if (selectedVoucher != null)
            {
                // Ako je popust procenat (npr. 0.12 = 12%)
                // order.TotalPrice -= order.TotalPrice * (decimal)activeVoucher.DiscountAmount;

                // Ako je popust fiksan iznos (npr. 500 dinara), koristi ovo umesto:
                order.TotalPrice -= (decimal)selectedVoucher.DiscountAmount;

                if (order.TotalPrice < 0)
                    order.TotalPrice = 0;

                selectedVoucher.Status = "Inactive";
                _unitOfWork.Vouchers.Update(selectedVoucher);
            }


            await _unitOfWork.Orders.AddAsync(order);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            return _mapper.Map<OrderResponseDto>(order);
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
