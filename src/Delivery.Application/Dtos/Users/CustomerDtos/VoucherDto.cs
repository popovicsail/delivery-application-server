namespace Delivery.Application.Dtos.Users.CustomerDtos
{
    public class VoucherDto
    {
        public string Name { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime ExpirationDate {get;set;}
    }
}
