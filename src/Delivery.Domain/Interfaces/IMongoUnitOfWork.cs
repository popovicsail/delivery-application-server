using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Interfaces
{
    public interface IMongoUnitOfWork
    {
        IBillRepository Bills { get; }
    }
}
