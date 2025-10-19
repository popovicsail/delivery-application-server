using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Dtos.Users.CustomerDtos.Responses;

public class CustomerDetailResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth {get;set;}
    public int LoyaltyPoints { get; set; }
    public IEnumerable<Allergen> Allergens { get; set; }
    public IEnumerable<Address> Addresses { get; set; }
    public IEnumerable<VoucherDto> Vouchers { get; set; }
}
