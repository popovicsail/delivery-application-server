using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.DishEntities;
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
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly IPdfService _pdfService;
        private IDeliveryTimeService _deliveryTimeService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IMongoUnitOfWork mongoUnitOfWork, IPdfService pdfService, IDeliveryTimeService deliveryTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _mongoUnitOfWork = mongoUnitOfWork;
            _pdfService = pdfService;
            _deliveryTimeService = deliveryTimeService;
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

        public async Task<OrderResponseDto> GetOneNotDraftAsync(ClaimsPrincipal User)
        {
            var user = _userManager.GetUserAsync(User).Result
                    ?? throw new Exception($"Not authorized");
            var customer = await _unitOfWork.Customers.GetOneAsync(user.Id)
                ?? throw new NotFoundException($"Customer with ID '{user.Id}' not found.");

            var orders = await _unitOfWork.Orders.GetOneNotDraftAsync(customer.Id);
            return _mapper.Map<OrderResponseDto>(orders);
        }


        public async Task<OrderDraftResponseDto>? GetDraftByCustomerAsync(ClaimsPrincipal User)
        {
            var user = _userManager.GetUserAsync(User).Result
                    ?? throw new Exception($"Not authorized");
            var customer = await _unitOfWork.Customers.GetOneAsync(user.Id)
                ?? throw new NotFoundException($"Customer with ID '{user.Id}' not found.");

            var order = await _unitOfWork.Orders.GetDraftByCustomerAsync(customer.Id);
            return _mapper.Map<OrderDraftResponseDto>(order);
        }

        public async Task<Guid> CreateItemsAsync(OrderItemsCreateRequestDto request, ClaimsPrincipal User)
        {
            if (request.Items.Count == 0)
            {
                throw new ArgumentException("Order is empty");
            }

            var user = _userManager.GetUserAsync(User).Result
                ?? throw new Exception($"Not authorized");

            // 1. Učitaj kupca
            var customer = await _unitOfWork.Customers.GetOneAsync(user.Id)
                ?? throw new NotFoundException($"Customer with ID '{user.Id}' not found.");

            var draftOrder = await _unitOfWork.Orders.GetDraftByCustomerAsync(customer.Id);

            // 2. Učitaj jela i ponude
            var dishIds = request.Items.Where(i => i.ItemType == "DISH").Select(i => i.Id).ToList();
            var dishes = await _unitOfWork.Dishes.GetByIdsWithAllergensAsync(dishIds);

            var offerIds = request.Items.Where(i => i.ItemType == "OFFER").Select(i => i.Id).ToList();
            var offers = await _unitOfWork.Offers.GetByIdsWithAllergensAsync(offerIds);

            // 3. Validacija alergena
            var customerAllergens = customer.Allergens.Select(a => a.Name).ToHashSet();
            foreach (var dish in dishes)
            {
                if (dish.Allergens!.Any(a => customerAllergens.Contains(a.Name)))
                    throw new BadRequestException($"Dish '{dish.Name}' contains allergens for this customer.");
            }

            foreach(var offer in offers)
            {
                foreach(var od in offer.OfferDishes)
                {
                    if (od.Dish!.Allergens!.Any(a => customerAllergens.Contains(a.Name)))
                        throw new BadRequestException($"Dish '{od.Dish.Name}' contains allergens for this customer.");
                }
            }


            //Grupisanje Offer-a jer nemaju dish-options za sad
            var finalReq = new List<OrderItemDto>();

            var groupedOffers = request.Items
                .Where(i => i.ItemType == "OFFER")
                .GroupBy(x => x.Id)
                .Select(g => new OrderItemDto
                {
                    Id = g.Key,
                    Quantity = g.Sum(x => x.Quantity),
                    ItemType = g.First().ItemType,
                }).ToList();

            var dishesReq = request.Items
                    .Where(i => i.ItemType == "DISH")
                    .ToList();

            finalReq = groupedOffers.Concat(dishesReq).ToList();

            // 4. Mapiraj Order iz DTO
            var order = new Order
            {
                CustomerId = customer.Id,
                FreeDelivery = false,
                Items = new List<OrderItem>(),
                RestaurantId = request.RestaurantId,
                CreatedAt = DateTime.UtcNow,
            };

            // 5. Mapiraj stavke i izračunaj cenu
            foreach (var itemDto in finalReq)
            {
                var item = new OrderItem();
                if (itemDto.ItemType == "OFFER")
                {
                    var offer = offers.First(o => o.Id == itemDto.Id);
                    itemDto.Name = offer.Name;
                    itemDto.DiscountExpireAt = offer.ExpiresAt;
                    item = _mapper.Map<OrderItem>(itemDto);
                    item.DishPrice = offer.Price;
                    item.OptionsPrice = 0;         //Za sad je ovako
                    order.FreeDelivery = order.FreeDelivery == false && offer.FreeDelivery;
                    
                }
                else if (itemDto.ItemType == "DISH")
                {
                    var dish = dishes.First(d => d.Id == itemDto.Id);
                    itemDto.Name = dish.Name;
                    itemDto.DiscountRate = dish.DiscountRate;
                    itemDto.DiscountExpireAt = dish.DiscountExpireAt;
                    item = _mapper.Map<OrderItem>(itemDto); // koristi CreateMap<OrderItemDto, OrderItem>
                    item.DishPrice = dish.Price;

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
                                        item.OptionsPrice += optionDto.Price * itemDto.Quantity;
                                    }
                                }
                            }
                        }
                    }
                    var optionIds = itemDto.DishOptionGroups?
                        .SelectMany(g => g.DishOptions)
                        .Select(o => o.Id)
                        .ToList() ?? new List<Guid>();

                    var options = await _unitOfWork.Dishes.GetDishOptionsByIdsAsync(optionIds);

                    item.DishOptions = options.ToList();
                }
                if (item.ItemType == "OFFER" && item.DiscountExpireAt != null && item.DiscountExpireAt < DateTime.UtcNow)
                    throw new ProductStateUnavailableException("Offer or Discount EXPIRED!");

                order.Items.Add(item);
            }

            // 6. Sačuvaj porudžbinu
            if (draftOrder != null)
            {
                foreach(var newItem in order.Items)
                {
                    if (newItem == null) continue;

                    if (newItem.Id == default) newItem.Id = Guid.NewGuid();

                    if (newItem != null && newItem.ItemType == "DISH")
                    {
                        newItem.OrderId = draftOrder.Id;
                        await _unitOfWork.OrderItems.AddAsync(newItem);
                        draftOrder.Items.Add(newItem);
                    }
                    if (newItem != null && newItem.ItemType == "OFFER")
                    {
                        if (draftOrder.Items.Any(i => i.OfferId == newItem.OfferId))
                        {
                            var existing = draftOrder.Items.FirstOrDefault(i => i.OfferId == newItem.OfferId);
                            existing.Quantity += newItem.Quantity;
                        }
                        else
                        {
                            newItem.OrderId = draftOrder.Id;
                            await _unitOfWork.OrderItems.AddAsync(newItem);
                            draftOrder.Items.Add(newItem);
                        }
                    }
                }

                if (draftOrder.FreeDelivery == false)
                {
                    draftOrder.FreeDelivery = order.FreeDelivery;
                }
            }
            else
            {
                order.Status = OrderStatus.Draft.ToString();
                await _unitOfWork.Orders.AddAsync(order);
            }
            await _unitOfWork.CompleteAsync();

            return draftOrder != null ? draftOrder.Id : order.Id;
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

        bool isWeatherGood = await _unitOfWork.AreasOfOperation.GetAreaConditionsByCity(order.Address.City);

        if (isWeatherGood == false)
        {
            order.TotalPrice += 200;
        }

        if (request.VoucherId.HasValue)
        {
            var voucher = customer.Vouchers.FirstOrDefault(v =>
                v.Id == request.VoucherId.Value && v.Status == "Active");

                if (voucher == null)
                    throw new BadRequestException("Selected voucher is invalid or inactive.");

                order.TotalPrice -= voucher.DiscountAmount;
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

        public async Task<byte[]?> UpdateStatusAsync(Guid orderId, string newStatus, int eta)
        {
            if (!Enum.TryParse<OrderStatus>(newStatus, true, out var statusEnum))
            {
                throw new ArgumentException($"Invalid order status: {newStatus}");
            }

            var order = await _unitOfWork.Orders.GetOneWithItemsAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID '{orderId}' not found.");

            // Ako restoran definiše vreme pripreme
            if (eta > 0)
            {
                order.TimeToPrepare = eta;
                order.EstimatedReadyAt = order.CreatedAt.AddMinutes(eta).ToUniversalTime();
            }

            order.Status = statusEnum.ToString();

            if (statusEnum == OrderStatus.Preuzeto)
            {
                order.DeliveryTimeMinutes = null;
                order.EstimatedDeliveryAt = null;
                order.DeliveryEstimateMessage = "Dostavljac je preuzeo dostavu.";
            }

            // 👇 Kada kurir preuzme porudžbinu → pozovi DeliveryTimeService
            if (statusEnum == OrderStatus.DostavaUToku)
            {
                var customerAddress = order.Address ?? order.Customer.Addresses.FirstOrDefault();
                if (customerAddress == null || !customerAddress.Latitude.HasValue || !customerAddress.Longitude.HasValue)
                {
                    order.DeliveryTimeMinutes = null;
                    order.EstimatedDeliveryAt = null;
                    order.DeliveryEstimateMessage = "Customer address coordinates are missing";
                }
                else
                { 
                    var minutes = await _deliveryTimeService.GetEstimatedDeliveryTimeMinutesAsync(
                        order.Restaurant.Address.Latitude ?? 0, order.Restaurant.Address.Longitude ?? 0,
                        customerAddress.Latitude.Value, customerAddress.Longitude.Value);

                    if (minutes.HasValue)
                    {
                        order.DeliveryTimeMinutes = minutes.Value;
                        order.EstimatedDeliveryAt = DateTime.UtcNow.AddMinutes(minutes.Value);
                        order.DeliveryEstimateMessage = $"Procena vremena dostave je {minutes.Value} minuta." +
                                                        $"Vreme dostave moze da varira u zavisnosti od uslova na putu.";
                    }
                    else
                    {
                        order.DeliveryTimeMinutes = null;
                        order.EstimatedDeliveryAt = null;
                        order.DeliveryEstimateMessage = "Procena vremena nije dostupna";
                    }
                }
            }

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync(); // 👈 opet koristi tvoj metod

            if (order.Status == OrderStatus.Zavrsena.ToString())
            {
                var bill = _mapper.Map<Bill>(order);

                await _mongoUnitOfWork.Bills.CreateAsync(bill);

                var billPdf = _pdfService.GenerateBillPdf(bill);

                return billPdf;
            }

            return null;
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

        public async Task<RestaurantRevenueStatisticsDto> GetRestaurantRevenueStatisticsAsync(Guid restaurantId, DateTime from, DateTime to)
        {
            var orders = await _unitOfWork.Orders.GetByRestaurantAndDateRangeAsync(restaurantId, from, to);
            
            var daily = orders
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new RestaurantDailyRevenueDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .OrderBy(x => x.Date)
                .ToList();

            double total = daily.Sum(d => d.Revenue);
            int orderCount = orders.Count();
            double average = orderCount > 0 ? total / orderCount : 0;

            return new RestaurantRevenueStatisticsDto
            {
                Daily = daily,
                TotalRevenue = total,
                OrderCount = orderCount,
                AverageOrderValue = average
            };
        }

        public async Task<DishRevenueStatisticsResponse> GetDishRevenueStatisticsAsync(Guid dishId, DateTime from, DateTime to)
        {
            var dish = await _unitOfWork.Dishes.GetOneAsync(dishId);
            if (dish == null)
            {
                throw new NotFoundException($"Dish with ID '{dishId}' not found.");
            }
            var menu = await _unitOfWork.Dishes.GetMenuAsync(dish.MenuId);
            if (menu == null)
            {
                throw new NotFoundException($"Menu with ID '{dish.MenuId}' not found.");
            }
            var orders = await _unitOfWork.Orders.GetByRestaurantAndDateRangeAsync(menu.RestaurantId, from, to);
           

            var dishEntries = orders
                .SelectMany(o => o.Items.Where(i => i.Id == dishId)
                    .Select(i => new
                    {
                        Date = o.CreatedAt.Date,
                        Revenue = i.DishPrice * i.Quantity
                    }))
                .ToList();

            var daily = dishEntries
                .GroupBy(x => x.Date)
                .Select(g => new DailyDishRevenue
                {
                    Date = g.Key,
                    Revenue = g.Sum(x => x.Revenue)
                })
                .OrderBy(d => d.Date)
                .ToList();

            double totalRevenue = daily.Sum(x => x.Revenue);
            int totalOrders = dishEntries.Count;
            double average = totalOrders > 0 ? totalRevenue / totalOrders : 0;

            return new DishRevenueStatisticsResponse
            {
                Daily = daily,
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                AverageRevenue = average
            };
        }

        public async Task<CanceledOrdersStatisticsDto> GetCanceledOrdersStatisticsAsync(Guid restaurantId, DateTime from, DateTime to)
        {
            var canceledOrders = await _unitOfWork.Orders.GetCanceledByRestaurantAndDateRangeAsync(restaurantId, from, to);

            var daily = canceledOrders
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new DailyCanceledOrdersDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            int totalCanceled = daily.Sum(d => d.Count);

            int totalDays = (to.Date - from.Date).Days + 1;
            double average = totalDays > 0 ? (double)totalCanceled / totalDays : 0;

            return new CanceledOrdersStatisticsDto
            {
                Daily = daily,
                TotalCanceled = totalCanceled,
                AverageCanceledPerDay = Math.Round(average, 2)
            };
        }
        
        public async Task<byte[]?> GetOrderBillPdfAsync(Guid orderId)
        {
            var bill = await _mongoUnitOfWork.Bills.GetByOrderIdAsync(orderId);

            if (bill == null)
            {
                return null;
            }

            var billPdf = _pdfService.GenerateBillPdf(bill);

            return billPdf;
        }
    }
}