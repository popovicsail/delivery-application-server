namespace Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Responses
{
    public class VoucherDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double DiscountAmount { get; set; }
        public string Status { get; set; }
        public Guid CustomerId { get; set; }
    }
}
