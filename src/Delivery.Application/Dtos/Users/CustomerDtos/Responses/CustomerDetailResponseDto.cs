using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Responses;

namespace Delivery.Application.Dtos.Users.CustomerDtos.Responses;

public class CustomerDetailResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IEnumerable<VoucherDetailResponseDto> Vouchers { get; set; }
}
