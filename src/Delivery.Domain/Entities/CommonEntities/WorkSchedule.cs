using System.Text.Json.Serialization;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.CommonEntities;

public class WorkSchedule
{
    public Guid Id { get; set; }
    public string Date { get; set; }
    public string WeekDay { get; set; }
    public TimeSpan WorkStart { get; set; }
    public TimeSpan WorkEnd { get; set; }

    public Guid CourierId { get; set; }
    public Courier Courier { get; set; } = default!;
}
