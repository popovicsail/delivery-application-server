using Delivery.Api.Contracts.Customers;
using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
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

        [HttpGet("my-addresses")]
        public async Task<IActionResult> GetMyAddresses()
        {
            var user = await _userManager.GetUserAsync(User);



            var customer = await _dbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (customer == null)
            {
                return Ok(new { Message = "Admin nema customer profil" });
            }

            if (await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                return Ok(new { Role = "Administrator" });
            }

            var addressDtos = customer.Addresses.Select(a => new AddressDto
            {
                Id = a.Id,
                StreetAndNumber = a.StreetAndNumber,
                City = a.City,
                PostalCode = a.PostalCode
            }).ToList();

            return Ok(addressDtos);
        }

        [HttpPost("my-addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);

            var newAddress = new Address
            {
                StreetAndNumber = request.StreetAndNumber,
                City = request.City,
                PostalCode = request.PostalCode
            };

            customer.Addresses.Add(newAddress);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("my-addresses/{addressId}")]
        public async Task<IActionResult> UpdateAddress(Guid addressId, [FromBody] AddressUpdateRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var customer = await _dbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);

            address.StreetAndNumber = request.StreetAndNumber;
            address.City = request.City;
            address.PostalCode = request.PostalCode;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("my-addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            var user = await _userManager.GetUserAsync(User);

            var customer = await _dbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);

            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("my-allergens")]
        public async Task<IActionResult> UpdateMyAllergens([FromBody] UpdateCustomerAllergensRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var customer = await _dbContext.Customers
                .Include(c => c.Allergens)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var newAllergens = await _dbContext.Allergens
                .Where(a => request.AllergenIds.Contains(a.Id))
                .ToListAsync();

            customer.Allergens = newAllergens;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("my-allergens")]
        public async Task<IActionResult> GetMyAllergens()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var customer = await _dbContext.Customers
                .Include(c => c.Allergens)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (customer == null) return NotFound();

            var allergenIds = customer.Allergens.Select(a => a.Id).ToList();

            return Ok(new { allergenIds });
        }
    }

}
