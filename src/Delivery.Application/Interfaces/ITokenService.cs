using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, List<string> roles);
    }
}
