using Delivery.Api.Contracts.Customers;
using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.DishEntities;
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
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public CustomersController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyCustomerProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found.");
            }
                
            var customer = await _dbContext.Customers
                .Include(c => c.Allergens)
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (customer == null)
            {
                return NotFound("Customer profile not found.");
            }

            var response = new CustomerProfileResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Allergens = customer.Allergens.Select(a => new AllergenDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList(),
                Adresses = customer.Addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
                    StreetAndNumber = a.StreetAndNumber,
                    City = a.City,
                    PostalCode = a.PostalCode
                }).ToList()
            };

            return Ok(response);
        }
    }

}
