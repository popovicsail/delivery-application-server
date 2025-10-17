using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Entities.CommonEntities;

public class BaseWorkSched
{
    public Guid Id { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public TimeSpan WorkDayStart { get; set; }
    public TimeSpan WorkDayEnd { get; set; }
    public TimeSpan? WeekendStart { get; set; }
    public TimeSpan? WeekendEnd { get; set; }
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
