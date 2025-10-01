using System.Security.Claims;
using Delivery.Api.Contracts;
using Delivery.Api.Contracts.Helper;
using Delivery.Api.Contracts.Restaurants;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public RestaurantsController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
           _dbContext = dbContext;
           _userManager = userManager;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _dbContext.Restaurants.ToListAsync();

            var response = restaurants.Select(r => new RestaurantSummaryResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();

            if (!response.Any())
            {
                return Ok(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(r => r.Owner)
                    .ThenInclude(o => o.User)
                .Include(r => r.Address)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var response = new RestaurantDetailResponse()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = new AddressDto
                {
                    StreetAndNumber = restaurant.Address.StreetAndNumber,
                    City = restaurant.Address.City,
                    PostalCode = restaurant.Address.PostalCode
                },
                Owner = new OwnerDto
                {
                    Id = restaurant.Owner.Id,
                    UserId = restaurant.Owner.UserId,
                    FirstName = restaurant.Owner.User.FirstName,
                    LastName = restaurant.Owner.User.LastName
                }
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestaurantCreateRequest createRequest)
        {
            var restaurant = new Restaurant()
            {
                Name = createRequest.Name,
                Description = createRequest.Description,
                Address = new Address()
                {
                    StreetAndNumber = createRequest.Address.StreetAndNumber,
                    City = createRequest.Address.City,
                    PostalCode = createRequest.Address.PostalCode
                },
                OwnerId = createRequest.OwnerId
            };

            _dbContext.Restaurants.Add(restaurant);

            await _dbContext.SaveChangesAsync();

            var restaurantUpdated = await _dbContext.Restaurants
                .Include(r => r.Owner)
                    .ThenInclude(o => o.User)
                .Include(r => r.Address)
                .FirstOrDefaultAsync(r => r.Id == restaurant.Id);

            var response = new RestaurantDetailResponse()
            {
                Id = restaurantUpdated.Id,
                Name = restaurantUpdated.Name,
                Description = restaurantUpdated.Description,
                Address = new AddressDto
                {
                    StreetAndNumber = restaurantUpdated.Address.StreetAndNumber,
                    City = restaurantUpdated.Address.City,
                    PostalCode = restaurantUpdated.Address.PostalCode
                },
                Owner = new OwnerDto
                {
                    Id = restaurantUpdated.Owner.Id,
                    UserId = restaurantUpdated.Owner.UserId,
                    FirstName = restaurantUpdated.Owner.User.FirstName,
                    LastName = restaurantUpdated.Owner.User.LastName
                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RestaurantUpdateRequest updateRequest, [FromRoute] Guid id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(r => r.Owner)
                    .ThenInclude(o => o.User)
                .Include(r => r.Address)              
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            restaurant.Name = updateRequest.Name;
            restaurant.Description = updateRequest.Description;
            restaurant.Address.StreetAndNumber = updateRequest.Address.StreetAndNumber;
            restaurant.Address.City = updateRequest.Address.City;
            restaurant.Address.PostalCode = updateRequest.Address.PostalCode;
            restaurant.OwnerId = updateRequest.OwnerId;

            await _dbContext.SaveChangesAsync();

            var response = new RestaurantDetailResponse()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = new AddressDto
                {
                    StreetAndNumber = restaurant.Address.StreetAndNumber,
                    City = restaurant.Address.City,
                    PostalCode = restaurant.Address.PostalCode
                },
                Owner = new OwnerDto
                {
                    Id = restaurant.Owner.Id,
                    UserId = restaurant.Owner.UserId,
                    FirstName = restaurant.Owner.User.FirstName,
                    LastName = restaurant.Owner.User.LastName
                }
            };

            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

             _dbContext.Restaurants.Remove(restaurant);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/menu")]
        public async Task<IActionResult> GetRestaurantMenu([FromRoute] Guid id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(r => r.Menus)
                    .ThenInclude(m => m.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var response = new RestaurantMenuResponse
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Menus = restaurant.Menus.Select(m => new MenuDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Dishes = m.Dishes.Select(d => new DishDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        Price = d.Price
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost("{restaurantId}/workers")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> RegisterWorker(Guid restaurantId, [FromBody] RegisterWorkerRequest request)
        {

            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);

            var user = await _userManager.GetUserAsync(User);

            var owner = await _dbContext.Owners.FirstOrDefaultAsync(o => o.UserId == user.Id);

            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            await _userManager.AddToRoleAsync(newUser, "Worker");

            var workerProfile = new Worker
            {
                UserId = newUser.Id,
                RestaurantId = restaurantId,
                IsSuspended = false
            };
            _dbContext.Workers.Add(workerProfile);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

}

