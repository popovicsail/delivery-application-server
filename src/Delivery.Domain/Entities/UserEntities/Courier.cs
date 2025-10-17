using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Domain.Entities.UserEntities;

public class Courier
{
    public Guid Id { get; set; }
    public string WorkStatus { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new HashSet<WorkSchedule>();

}
