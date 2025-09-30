namespace Delivery.Api.Contracts.Helper
{
    public class DishOptionGroupDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public Guid DishId { get; set; }

        public List<DishOptionDto> DishOptions { get; set; }
    }
}
