using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.OwnerDtos.Requests;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterCourierAsync(CourierCreateRequestDto request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ProfilePictureUrl = DefaultAvatar.Base64
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            throw new BadRequestException("Greška pri kreiranju kurira: " +
                string.Join(", ", createResult.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Courier");

        var courierProfile = new Courier
        {
            UserId = user.Id,
            WorkStatus = "NEAKTIVAN"
        };

        await _unitOfWork.Couriers.AddAsync(courierProfile);
        await _unitOfWork.CompleteAsync();
    }

    public async Task RegisterOwnerAsync(OwnerCreateRequestDto request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ProfilePictureUrl = DefaultAvatar.Base64
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            throw new BadRequestException("Greška pri kreiranju vlasnika: " +
                string.Join(", ", createResult.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Owner");

        var ownerProfile = new Owner { UserId = user.Id };

        await _unitOfWork.Owners.AddAsync(ownerProfile);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new NotFoundException($"Korisnik sa ID '{userId}' nije pronađen.");

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Contains("Administrator"))
            throw new ForbiddenException("Nije dozvoljeno brisanje administratora.");

        var deleteResult = await _userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
            throw new BadRequestException("Greška pri brisanju korisnika: " +
                string.Join(", ", deleteResult.Errors.Select(e => e.Description)));

        await _unitOfWork.CompleteAsync();
    }
}
