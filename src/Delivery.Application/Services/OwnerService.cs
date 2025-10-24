using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Delivery.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public OwnerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<OwnerSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Owner> owners = await _unitOfWork.Owners.GetAllAsync();
        return _mapper.Map<List<OwnerSummaryResponseDto>>(owners.ToList());
    }

    public async Task<OwnerDetailResponseDto?> GetOneAsync(Guid id)
    {
        var owner = await _unitOfWork.Owners.GetOneAsync(id);

        if (owner == null)
        {
            throw new NotFoundException($"Owner with ID '{id}' was not found.");
        }

        return _mapper.Map<OwnerDetailResponseDto>(owner);
    }

    public async Task<OwnerDetailResponseDto> AddAsync(OwnerCreateRequestDto request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException("ERROR: Error while creating new owner.");
        }

        await _userManager.AddToRoleAsync(user, "Owner");

        var owner = new Owner { UserId = user.Id };
        await _unitOfWork.Owners.AddAsync(owner);
        await _unitOfWork.CompleteAsync();

        var createdOwner = await _unitOfWork.Owners.GetOneAsync(owner.Id);

        return _mapper.Map<OwnerDetailResponseDto>(createdOwner);
    }

    public async Task UpdateAsync(Guid id, OwnerUpdateRequestDto ownerDto)
    {
        var owner = await _unitOfWork.Owners.GetOneAsync(id);

        if (owner == null)
        {
            throw new NotFoundException($"Owner with ID '{id}' was not found.");
        }

        _mapper.Map(ownerDto, owner);

        _unitOfWork.Owners.Update(owner);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var owner = await _unitOfWork.Owners.GetOneAsync(id);

        if (owner == null)
        {
            throw new NotFoundException($"Owner with ID '{id}' was not found.");
        }

        _unitOfWork.Owners.Update(owner);

        await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> GetMenuPermissionAsync(ClaimsPrincipal User, Guid menuId)
    {
        var user = await _userManager.GetUserAsync(User);
        bool permitGranted = await _unitOfWork.Owners.GetMenuPermissionAsync(user!, menuId);
        if (permitGranted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
