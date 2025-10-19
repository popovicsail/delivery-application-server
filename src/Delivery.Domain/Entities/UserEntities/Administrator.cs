namespace Delivery.Domain.Entities.UserEntities;

public class Administrator
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
