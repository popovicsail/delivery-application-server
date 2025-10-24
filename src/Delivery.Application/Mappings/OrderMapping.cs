using AutoMapper;
using Delivery.Application.Dtos.OrderDtos.Requests;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Mappings
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            // CreateOrderRequestDto -> Order
            CreateMap<CreateOrderRequestDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "KREIRANA"))
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore()); // puni se ručno u servisu

            // Order -> OrderResponseDto
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName))
                .ForMember(dest => dest.DeliveryAddress,
                    opt => opt.MapFrom(src => src.Address.StreetAndNumber + ", " + src.Address.City))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            // OrderItem -> OrderItemDto
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Dish.Name));
        }
    }
}
