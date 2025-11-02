using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.OrderEntities.Enums
{
    public enum OrderStatus
    {
        NaCekanju,
        Prihvacena,
        Odbijena,
        CekaSePreuzimanje,
        Preuzeto,
        DostavaUToku,
        Zavrsena
    }
}
