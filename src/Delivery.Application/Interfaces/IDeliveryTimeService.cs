using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Interfaces
{
    public interface IDeliveryTimeService
    {
        Task<int?> GetEstimatedDeliveryTimeMinutesAsync(double restaurantLat, double restaurantLon, double customerLat, double customerLon);
    }
}
