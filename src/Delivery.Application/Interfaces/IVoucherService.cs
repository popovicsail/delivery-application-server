using System.Security.Claims;
using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Responses;
using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Requests;
using Delivery.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Delivery.Application.Interfaces;

public interface IVoucherService
{
    Task<VoucherDetailResponseDto?> GetOneAsync(Guid id);
    Task<VoucherDetailResponseDto> AddAsync(VoucherCreateRequestDto request);
    Task UpdateAsync(Guid id, VoucherUpdateRequestDto request);
    Task DeleteAsync(Guid id);
    Task UseVoucherAsync(Guid id, Guid userId);

    Task VoucherExpirationDateCheckerBackgroundJobAsync();

}
