using Delivery.Api.Contracts.Helper;
using Delivery.Api.Contracts.Users;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            var response = new List<UserSummaryResponse>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                response.Add(new UserSummaryResponse
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = roles
                });
            }

            return Ok(response);
        }

        [HttpGet("owners")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _dbContext.Owners
                .Include(o => o.User)
                .ToListAsync();

            var response = new List<OwnerDto>();

            foreach (var o in owners)
            {
                response.Add(new OwnerDto
                {
                    Id = o.Id,
                    FirstName = o.User.FirstName,
                    LastName = o.User.LastName,
                    UserId = o.User.Id
                });
            }

            return Ok(response);
        }
    }
}
