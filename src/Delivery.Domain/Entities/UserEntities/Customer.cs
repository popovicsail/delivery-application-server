using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Domain.Entities.UserEntities;

public class Customer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    public virtual ICollection<Allergen> Allergens { get; set; } = new HashSet<Allergen>();
    public virtual ICollection<Voucher> Vouchers { get; set; } = new HashSet<Voucher>();
}

