using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.DishDtos;

namespace Delivery.Application.Dtos.OrderDtos.Responses
{
    public class OrderItemDto
    {
        public Guid DishId { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<DishOptionGroupDto> DishOptionGroups { get; set; } = new();
    }
}
