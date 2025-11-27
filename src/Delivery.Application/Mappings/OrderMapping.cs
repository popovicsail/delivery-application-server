using AutoMapper;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.OrderEntities.Enums;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            // 🔹 3-step: CreateOrderItemsDto → Order
            CreateMap<OrderItemsCreateRequestDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => OrderStatus.Draft.ToString()))
                .ForMember(dest => dest.Items, opt => opt.Ignore()) // puni se ručno
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

            // 🔹 OrderItemDto → OrderItem
            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.DishPrice, opt => opt.Ignore()) // računa se ručno
                .ForMember(dest => dest.OptionsPrice, opt => opt.Ignore()) // računa se ručno
                .ForMember(dest => dest.DishId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DishOptions, opt => opt.Ignore())
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.Condition(src => src.Name != null);
                    opt.MapFrom(src => src.Name);
                });

            // 🔹 Order → OrderResponseDto
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName))
                .ForMember(dest => dest.DeliveryAddress,
                    opt => opt.MapFrom(src => src.Address != null
                        ? src.Address.StreetAndNumber + ", " + src.Address.City
                        : ""))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EstimatedReadyAt, opt => opt.MapFrom(src => src.EstimatedReadyAt))
                .ForMember(dest => dest.DeliveryTimeMinutes, opt => opt.MapFrom(src => src.DeliveryTimeMinutes))
                .ForMember(dest => dest.EstimatedDeliveryAt, opt => opt.MapFrom(src => src.EstimatedDeliveryAt))
                .ForMember(dest => dest.DeliveryEstimateMessage, opt => opt.MapFrom(src => src.DeliveryEstimateMessage));

            // 🔹 OrderItem → OrderItemDto
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Order, OrderDraftResponseDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Items.Count > 0
                ? src.Items.Sum(i => (i.DishPrice - (i.DiscountExpireAt > DateTime.UtcNow && i.DiscountRate != 0 ? i.DiscountRate * i.DishPrice : 0) + i.OptionsPrice) * i.Quantity) 
                : 0));

            CreateMap<OrderItem, OrderItemSummaryResponse>();
            
            CreateMap<Order, Bill>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.User.Email))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.User.FirstName))
                .ForMember(dest => dest.IssuedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<OrderItem, BillItem>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.DishPrice));
        }
    }
}