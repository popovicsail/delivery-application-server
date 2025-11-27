using System.Net;
using System.Text;
using AutoMapper;
using Delivery.Application.Dtos.Users.AuthDtos.Requests;
using Delivery.Application.Dtos.Users.AuthDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.WebEncoders;

namespace Delivery.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork, IConfiguration configuration, IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _emailSender = emailSender;
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

    public async Task<LoginResponseDto> GoogleLoginAsync(GoogleLoginRequestDto googleRequest)
    {
        var googleClientId = _configuration["Authentication:Google:ClientId"];
        if (string.IsNullOrEmpty(googleClientId))
        {
            throw new InvalidOperationException("ERROR: Google ClientID nije konfigurisan na serveru.");
        }

        GoogleJsonWebSignature.Payload payload;
        try
        {
            var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = [googleClientId]
            };
            payload = await GoogleJsonWebSignature.ValidateAsync(googleRequest.GoogleToken, validationSettings);
        }
        catch (Exception ex)
        {
            throw new UnauthorizedAccessException("ERROR: Google autentifikacija nije uspela.", ex);
        }

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new User
            {
                UserName = payload.Email,
                Email = payload.Email,
                FirstName = payload.Name.Split(" ")[0],
                LastName = payload.Name.Split(" ")[1],
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user);
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
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles.ToList());

        var loginResponse = new LoginResponseDto
        {
            Token = token
        };

        return loginResponse;
    }

    public async Task RegisterAsync(RegisterRequestDto registerRequest)
    {
        User user = _mapper.Map<User>(registerRequest);

        user.EmailConfirmed = false;

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

        await SendActivationEmailAsync(user);
    }

    private async Task SendActivationEmailAsync(User user)
    {
        try
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);
            var frontendUrl = _configuration["FrontendBaseUrl"];

            if (string.IsNullOrEmpty(frontendUrl))
            {
                throw new InvalidOperationException("FrontendBaseUrl nije konfigurisan u appsettings.");
            }

            var activationLink = $"{frontendUrl}/activate-account?email={user.Email}&token={encodedToken}";
            var emailSubject = "Aktivirajte Vaš Nalog - Delivery App";
            var htmlMessage = $@"
                <h1>Dobrodošli u Delivery App!</h1>
                <p>Molimo Vas da aktivirate Vaš nalog klikom na link ispod:</p>
                <a href='{activationLink}'>Aktiviraj Nalog</a>";

            await _emailSender.SendEmailAsync(user.Email, emailSubject, htmlMessage);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Greška pri slanju emaila za potvrdu: {ex.Message}");
        }
    }

    public async Task ActivateAccountAsync(ActivateAccountRequestDto activateRequest)
    {
        if (string.IsNullOrEmpty(activateRequest.Email) || string.IsNullOrEmpty(activateRequest.Token))
        {
            throw new BadRequestException("Email i token su obavezni.");
        }

        var user = await _userManager.FindByEmailAsync(activateRequest.Email);
        if (user == null)
        {
            throw new NotFoundException("Korisnik nije pronađen.");
        }

        var tokenBytes = WebEncoders.Base64UrlDecode(activateRequest.Token);
        var decodedToken = Encoding.UTF8.GetString(tokenBytes);

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Aktivacija nije uspela: {errors}");
        }
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequestDto forgotRequest)
    {
        if (string.IsNullOrEmpty(forgotRequest.Email))
        {
            throw new BadRequestException("Email je obavezan.");
        }

        var user = await _userManager.FindByEmailAsync(forgotRequest.Email);

        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return;
        }

        try
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            var frontendUrl = _configuration["FrontendBaseUrl"];
            if (string.IsNullOrEmpty(frontendUrl))
            {
                throw new InvalidOperationException("FrontendBaseUrl nije konfigurisan u appsettings.");
            }

            var resetLink = $"{frontendUrl}/reset-password?email={user.Email}&token={encodedToken}";

            var emailSubject = "Resetovanje Lozinke - Delivery App";
            var htmlMessage = $@"
                <h1>Zahtev za resetovanje lozinke</h1>
                <p>Dobili smo zahtev za resetovanje Vaše lozinke. Ako niste Vi podneli ovaj zahtev, ignorišite ovaj email.</p>
                <p>Za promenu lozinke, kliknite na link ispod:</p>
                <a href='{resetLink}' style='padding: 10px 15px; background-color: #dc3545; color: white; text-decoration: none; border-radius: 5px;'>
                    Resetuj Lozinku
                </a>
                <br>
                <p>Ako link ne radi, iskopirajte ovu adresu u Vaš pretraživač:</p>
                <p>{resetLink}</p>";

            await _emailSender.SendEmailAsync(user.Email, emailSubject, htmlMessage);
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public async Task ResetPasswordAsync(ResetPasswordRequestDto resetRequest)
    {
        if (resetRequest == null || string.IsNullOrEmpty(resetRequest.Email) || string.IsNullOrEmpty(resetRequest.Token))
        {
            throw new BadRequestException("ERROR: Invalid request.");
        }
        var user = await _userManager.FindByEmailAsync(resetRequest.Email);

        if (user == null)
        {
            throw new BadRequestException("ERROR: Unsuccessful password change. Try again.");
        }

        var tokenBytes = WebEncoders.Base64UrlDecode(resetRequest.Token);
        var decodedToken = Encoding.UTF8.GetString(tokenBytes);

        var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetRequest.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"ERROR: Unsuccessful password change: {errors}");
        }
    }
}