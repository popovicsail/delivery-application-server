using AutoMapper;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.OrderEntities;
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
            order.Status = "Pending";

            // 7. Primeni vaučer ako postoji
            var activeVoucher = customer.Vouchers
                .Where(v => v.Status == "Active")
                .OrderByDescending(v => v.DiscountAmount) // uzmi najveći popust
                .FirstOrDefault();

            if (activeVoucher != null)
            {
                // Ako je popust procenat (npr. 0.12 = 12%)
                // order.TotalPrice -= order.TotalPrice * (decimal)activeVoucher.DiscountAmount;

                // Ako je popust fiksan iznos (npr. 500 dinara), koristi ovo umesto:
                order.TotalPrice -= (decimal)activeVoucher.DiscountAmount;

                if (order.TotalPrice < 0)
                    order.TotalPrice = 0;

                // Deaktiviraj vaučer nakon korišćenja
                activeVoucher.Status = "Inactive";
                _unitOfWork.Vouchers.Update(activeVoucher);
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

        public async Task UpdateStatusAsync(Guid orderId, string newStatus)
        {
            var order = await _unitOfWork.Orders.GetOneWithItemsAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");

            order.Status = newStatus;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync(); // 👈 opet koristi tvoj metod
        }
    }
}
