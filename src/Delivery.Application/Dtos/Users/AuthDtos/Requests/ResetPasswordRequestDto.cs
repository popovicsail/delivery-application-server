using System.ComponentModel.DataAnnotations;

namespace Delivery.Application.Dtos.Users.AuthDtos.Requests;

public class ResetPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; }
}