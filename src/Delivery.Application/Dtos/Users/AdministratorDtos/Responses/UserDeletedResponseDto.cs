using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.Users.AdministratorDtos.Responses
{
    public class UserDeletedResponseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
