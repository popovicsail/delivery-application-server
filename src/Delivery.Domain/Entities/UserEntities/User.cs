using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Api.Contracts.Auth;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Domain.Entities.UserEntities
{
    public class User : IdentityUser<Guid>
    {
        // public Guid Id

        // public string UserName

        // public string Email
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureBase64 { get; set; }

        //public ICollection Roles - Pristupa se preko UserManager<User>
    }
}
