using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;

namespace Delivery.Application.Interfaces
{
    public interface IAdminService
    {
        Task RegisterCourierAsync(CourierCreateRequestDto request);
        Task RegisterOwnerAsync(OwnerCreateRequestDto request);
        Task DeleteUserAsync(Guid userId);
    }
}
