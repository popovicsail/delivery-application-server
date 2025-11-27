namespace Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Requests;

public class VoucherCreateRequestDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime DateIssued { get; set; }
    public DateTime ExpirationDate { get; set; }
    public double DiscountAmount { get; set; }
    public string Status { get; set; }
    public Guid CustomerId { get; set; }
}

