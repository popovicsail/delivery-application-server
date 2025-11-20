using System.Security.Claims;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Delivery.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetByRestaurantAsync(Guid restaurantId)
        {
            var orders = await _unitOfWork.Orders.GetByRestaurant(restaurantId);

            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<(IEnumerable<OrderResponseDto> Items, int TotalCount)> GetByCourierAsync(
        Guid courierId,
        DateTime? from = null,
        DateTime? to = null,
        int page = 1,
        int pageSize = 10)
        {
            var query = _unitOfWork.Orders.GetByCourier(courierId, from, to);

            var totalCount = await query.CountAsync(); // ukupan broj pre paginacije

            var pageItems = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<OrderResponseDto>>(pageItems);

            return (mapped, totalCount);
        }




        public async Task<(IEnumerable<OrderResponseDto> Items, int TotalCount)> GetByCustomerAsync(
        Guid customerId,
        int page = 1,
        int pageSize = 10)
        {
            var query = _unitOfWork.Orders.GetByCustomer(customerId);

            var totalCount = await query.CountAsync(); // ukupan broj pre paginacije

            var pageItems = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<OrderResponseDto>>(pageItems);

            return (mapped, totalCount);
        }






        public async Task<OrderDraftResponseDto>? GetDraftByCustomerAsync(ClaimsPrincipal User)
        {
            var user = _userManager.GetUserAsync(User).Result
                    ?? throw new Exception($"Not authorized");
            var customer = await _unitOfWork.Customers.GetOneAsync(user.Id)
                ?? throw new NotFoundException($"Customer with ID '{user.Id}' not found.");

            var orders = await _unitOfWork.Orders.GetDraftByCustomerAsync(customer.Id);
            return _mapper.Map<OrderDraftResponseDto>(orders);
        }

        public async Task<Guid> CreateItemsAsync(OrderItemsCreateRequestDto request, ClaimsPrincipal User)
        {
            var user = _userManager.GetUserAsync(User).Result
                ?? throw new Exception($"Not authorized");

            // 1. Učitaj kupca
            var customer = await _unitOfWork.Customers.GetOneAsync(user.Id)
                ?? throw new NotFoundException($"Customer with ID '{user.Id}' not found.");

            var draftOrder = await GetDraftByCustomerAsync(User);

            // 2. Učitaj jela
            var dishIds = request.Items.Select(i => i.Id).ToList();
            var dishes = await _unitOfWork.Dishes.GetByIdsWithAllergensAsync(dishIds);

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
                var dish = dishes.First(d => d.Id == itemDto.Id);
                itemDto.Name = dish.Name;
                var item = _mapper.Map<OrderItem>(itemDto); // koristi CreateMap<OrderItemDto, OrderItem>
                item.Price = itemDto.Price * itemDto.Quantity;
                //Izracunavanje cena za opcije
                if (itemDto.DishOptionGroups != null && itemDto.DishOptionGroups.Count > 0)
                {
                    foreach (var optionGroupDto in itemDto.DishOptionGroups)
                    {
                        if (optionGroupDto.DishOptions != null && optionGroupDto.DishOptions.Count > 0)
                        {
                            foreach (var optionDto in optionGroupDto.DishOptions)
                            {
                                optionDto.Name = dish.DishOptionGroups
                                    .First(og => og.Id == optionGroupDto.Id)
                                    .DishOptions.First(o => o.Id == optionDto.Id).Name;
                                if (optionDto != null)
                                {
                                    item.Price += (decimal)(optionDto.Price * itemDto.Quantity);
                                }
                            }
                        }
                    }
                }

                var optionIds = itemDto.DishOptionGroups?
                    .SelectMany(g => g.DishOptions)
                    .Select(o => o.Id)
                    .ToList() ?? new List<Guid>();

                item.DishOptions = await _unitOfWork.Dishes.GetDishOptionsByIdsAsync(optionIds);
                order.Items.Add(item);
            }
            order.CustomerId = customer.Id;

            // 6. Sačuvaj porudžbinu
            if (draftOrder != null)
            {
                foreach(var newItem in order.Items)
                {
                    newItem.OrderId = draftOrder.Id;
                    await _unitOfWork.OrderItems.AddAsync(newItem);
                }
            }
            else
            {
                await _unitOfWork.Orders.AddAsync(order);
            }
            await _unitOfWork.CompleteAsync();

            return order.Id;
        }



        public async Task<OrderResponseDto> UpdateDetailsAsync(Guid orderId, OrderUpdateDetailsDto request)
        {
            var order = await _unitOfWork.Orders.GetOneWithCustomerAsync(orderId)
                ?? throw new NotFoundException($"Order with ID '{orderId}' not found.");

            var customer = order.Customer;

            if (!customer.Addresses.Any(a => a.Id == request.AddressId))
                throw new BadRequestException("Invalid delivery address for this customer.");

            order.AddressId = request.AddressId;
            order.SetTotalPrice();

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

            order.Status = OrderStatus.NaCekanju.ToString();
            _unitOfWork.Orders.Update(order);

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResponseDto>(order);
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

        public async Task DeleteAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetOneAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");
            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteItemAsync(Guid orderItemId)
        {
            var orderItem = await _unitOfWork.OrderItems.GetOneAsync(orderItemId);
            if (orderItem == null)
                throw new NotFoundException($"Order item with ID '{orderItemId}' not found.");
            _unitOfWork.OrderItems.Delete(orderItem);
            await _unitOfWork.CompleteAsync();
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
