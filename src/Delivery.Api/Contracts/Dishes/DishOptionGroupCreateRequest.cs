using Delivery.Api.Contracts.Helper;

namespace Delivery.Api.Contracts.Dishes
{
    public class DishOptionGroupCreateRequest
    {
        public string Name { get; set; }
        public Guid DishId { get; set; }
        public List<DishOptionDto> DishOptions { get; set; } = new();
    }
}
