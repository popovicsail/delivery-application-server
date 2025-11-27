using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.Users.AuthDtos.Requests
{
    public class ActivateAccountRequestDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}