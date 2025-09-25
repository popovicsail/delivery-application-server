using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.UserEntities
{
    public class Administrator
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
