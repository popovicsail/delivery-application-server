using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string? ProfilePictureBase64 { get; set; }   // samo Base64 string
        public string? ProfilePictureMimeType { get; set; } // npr. "image/png"


        //public ICollection Roles - Pristupa se preko UserManager<User>
    }
}
