using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Exceptions
{
    public class ProductStateUnavailableException : Exception
    {
        public ProductStateUnavailableException(string message) : base(message) { }
    }
}
