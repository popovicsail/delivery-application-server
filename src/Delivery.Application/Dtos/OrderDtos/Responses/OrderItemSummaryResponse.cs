using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.DishDtos;

namespace Delivery.Application.Dtos.OrderDtos.Responses
{
    public class OrderItemSummaryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public List<DishOptionDto> DishOptions { get; set; } = new List<DishOptionDto>();
    }
}
