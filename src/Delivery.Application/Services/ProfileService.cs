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
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return response;
        }

        public async Task<ProfileResponseDto> UpdateAsync(Guid userId, ProfileUpdateRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException("ERROR: Profile response");
            }

            var response = _mapper.Map<ProfileResponseDto>(user);

            response.Roles = await _userManager.GetRolesAsync(user);

            return response;
        }
    }
}
