using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Application.Dtos.OfferDtos.Requests
{
    public class OfferCreateRequestDto
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public double Price { get; set; }
        public bool FreeDelivery { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
    }
}
