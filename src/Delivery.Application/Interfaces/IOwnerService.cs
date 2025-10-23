﻿using System.Security.Claims;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IOwnerService
{
    Task<IEnumerable<OwnerSummaryResponseDto>> GetAllAsync();
    Task<OwnerDetailResponseDto?> GetOneAsync(Guid id);
    Task<OwnerDetailResponseDto> AddAsync(OwnerCreateRequestDto request);
    Task UpdateAsync(Guid id, OwnerUpdateRequestDto request);
    Task DeleteAsync(Guid id);
    Task<bool> GetMenuPermissionAsync(ClaimsPrincipal User, Guid menuId);
}
