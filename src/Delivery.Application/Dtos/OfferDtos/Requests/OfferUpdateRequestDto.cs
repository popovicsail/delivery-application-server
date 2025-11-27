using System.ComponentModel.DataAnnotations;

namespace Delivery.Application.Dtos.OfferDtos.Requests
{
    public class OfferUpdateRequestDto
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public double Price { get; set; }
        public bool FreeDelivery { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
    }
}
