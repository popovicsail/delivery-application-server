using AutoMapper;
using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.VoucherDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;

namespace Delivery.Application.Services;

public class VoucherService : IVoucherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VoucherService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<VoucherDetailResponseDto?> GetOneAsync(Guid id)
    {
        var voucher = await _unitOfWork.Vouchers.GetOneAsync(id);

        if (voucher == null)
        {
            throw new NotFoundException($"Voucher with ID '{id}' was not found.");
        }

        return _mapper.Map<VoucherDetailResponseDto>(voucher);
    }

    public async Task<VoucherDetailResponseDto> AddAsync(VoucherCreateRequestDto request)
    {
        var voucher = _mapper.Map<Voucher>(request);

        await _unitOfWork.Vouchers.AddAsync(voucher);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<VoucherDetailResponseDto>(voucher);
    }

    public async Task UpdateAsync(Guid id, VoucherUpdateRequestDto request)
    {
        var voucher = await _unitOfWork.Vouchers.GetOneAsync(id);

        if (voucher == null)
        {
            throw new NotFoundException($"Voucher with ID '{id}' was not found.");
        }

        _mapper.Map(request, voucher);

        _unitOfWork.Vouchers.Update(voucher);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var voucher = await _unitOfWork.Vouchers.GetOneAsync(id);

        if (voucher == null)
        {
            throw new NotFoundException($"ERROR: Voucher with ID '{id}' was not found.");
        }

        _unitOfWork.Vouchers.Delete(voucher);

        await _unitOfWork.CompleteAsync();
    }

    public async Task UseVoucherAsync(Guid id, Guid userId)
    {
        var customer = await _unitOfWork.Customers.GetOneAsync(userId);

        var voucher = await _unitOfWork.Vouchers.GetOneAsync(id);
        if (voucher == null)
        {
            throw new NotFoundException($"ERROR: Voucher with ID '{id}' was not found.");
        }

        if (voucher.CustomerId != customer.Id)
        {
            throw new ForbiddenException("ERROR: Unauthorized.");
        }

        voucher.Status = "Used";

        await _unitOfWork.CompleteAsync();

        return;
    }

    public async Task VoucherExpirationDateCheckerBackgroundJobAsync()
    {
        var vouchers = await _unitOfWork.Vouchers.GetActiveVouchersAsync();

        if (!vouchers.Any())
        {
            return;
        }

        foreach (var voucher in vouchers)
        {
            voucher.Status = "Inactive";
        }

        await _unitOfWork.CompleteAsync();
    }
}
