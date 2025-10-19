using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.Users.CustomerDtos
{
    public class VoucherDto
    {
        public string Name { get; set; }
        public DateTime DateIssued { get; set; }
        public double DiscountAmount { get; set; }
    }
}
