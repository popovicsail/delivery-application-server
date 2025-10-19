using AutoMapper;
using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.AuthDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.UserName);
        if (user == null)
        {
            throw new BadRequestException("ERROR: Invalid username or password.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new BadRequestException("ERROR: Invalid username or password.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles.ToList());

        var loginResponse = new LoginResponseDto
        {
            Token = token
        };

        return loginResponse;
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequest)
    {
        User user = _mapper.Map<User>(registerRequest);

        var createResult = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!createResult.Succeeded)
        {
            throw new BadRequestException("ERROR: Something went wrong while creating the account.");
        }

        await _userManager.AddToRoleAsync(user, "Customer");

        var customerProfile = new Customer
        {
            UserId = user.Id
        };
        await _unitOfWork.Customers.AddAsync(customerProfile);

        await _unitOfWork.CompleteAsync();

        var roles = new List<string>
        {
            "Customer"
        };
        var token = _tokenService.CreateToken(user, roles);

        var registerResponse = new RegisterResponseDto
        {
            Token = token
        };

        return registerResponse;
    }
}
