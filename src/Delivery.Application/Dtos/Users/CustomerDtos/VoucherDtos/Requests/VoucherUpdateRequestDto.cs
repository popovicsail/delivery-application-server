namespace Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Requests;

public class VoucherUpdateRequestDto
{
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
    public double DiscountAmount { get; set; }
    public string Status { get; set; }
}

