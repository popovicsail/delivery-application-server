using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.Users.ProfileDtos.Requests;
using Delivery.Application.Dtos.Users.ProfileDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ProfileResponseDto> GetOneAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var response = new ProfileResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList(),
                ProfilePictureBase64 = user.ProfilePictureBase64
            };

            return response;
        }

        public async Task<ProfileResponseDto> UpdateAsync(ClaimsPrincipal principal, ProfileUpdateRequestDto request)
        {
            var user = await _userManager.GetUserAsync(principal);
            if (user == null) throw new NotFoundException("User not found");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            if (request.ProfilePictureBase64 is { Length: > 0 })
            {
                var allowedMimeTypes = new[] { "image/png", "image/jpeg" };
                var contentType = request.ProfilePictureBase64.ContentType.ToLower();

                if (!allowedMimeTypes.Contains(contentType))
                    throw new BadRequestException("Only PNG and JPEG images are allowed.");

                await using var ms = new MemoryStream();
                await request.ProfilePictureBase64.CopyToAsync(ms);
                var fileBytes = ms.ToArray();

                user.ProfilePictureBase64 = $"data:{contentType};base64,{Convert.ToBase64String(fileBytes)}";
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new BadRequestException("Update failed");

            var roles = await _userManager.GetRolesAsync(user);

            return new ProfileResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureBase64 = user.ProfilePictureBase64,
                Roles = roles.ToList()
            };
        }
    }
}
