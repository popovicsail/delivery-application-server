namespace Delivery.Domain.Entities.UserEntities;

public class Voucher
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateIssued { get; set; }
    public double DiscountAmount { get; set; }
    public bool Active { get; set; }
    public Guid CustomerId { get; set; }
}
