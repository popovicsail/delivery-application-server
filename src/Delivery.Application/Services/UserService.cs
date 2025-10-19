using Delivery.Application.Dtos.Users.UserDtos.Responses;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserSummaryResponseDto>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var response = new List<UserSummaryResponseDto>();

        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            response.Add(new UserSummaryResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Roles = roles
            });
        }

        return response;

        //TODO: Postoji neki fensi način da se preko AutoMappera reše i asinhroni DTO-ovi, ali mi za sada deluje komplikovano.
        //TODO: Istražiti advanced LINQ metodu za dobavljanje usera sa roleovima da bi se izbegao N+1 problem.
    }
}
