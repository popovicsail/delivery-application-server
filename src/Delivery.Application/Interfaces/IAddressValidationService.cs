using Delivery.Application.Dtos.AdressValidationDtos.Responses;
﻿using System.Threading.Tasks;
using Delivery.Application.Dtos.AdressValidationDtos.Responses;
using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Interfaces
{
    public interface IAddressValidationService
    {
        Task<AddressValidationResultDto> ValidateAsync(string address, string restaurantCity);
        Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address);
        Task<Address?> GetAddressFromCoordinatesAsync(double latitude, double longitude);
    }
}
