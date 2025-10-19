namespace Delivery.Application.Dtos.Users.CustomerDtos
{
    public class VoucherDto
    {
        public string Name { get; set; }
        public DateTime DateIssued { get; set; }
        public double DiscountAmount { get; set; }
    }
}
