using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.RestaurantEntities;

public class Restaurant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }

    public Guid AddressId { get; set; }
    public virtual Address Address { get; set; }  

    public Guid OwnerId { get; set; }
    public virtual Owner? Owner { get; set; }

    public virtual BaseWorkSched? BaseWorkSched { get; set; }

    public string Image { get; set; }

    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new HashSet<WorkSchedule>();
    public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();
    public virtual ICollection<Menu> Menus { get; set; } = new HashSet<Menu>();

}