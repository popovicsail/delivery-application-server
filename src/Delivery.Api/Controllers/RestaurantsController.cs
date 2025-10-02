using Delivery.Api.Contracts;
using Delivery.Api.Contracts.Helper;
using Delivery.Api.Contracts.Restaurants;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Microsoft.EntityFrameworkCore;


namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RestaurantsController(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _dbContext.Restaurants
                .Include(r => r.Owner)
                    .ThenInclude(o => o.User)
                .Include(r => r.Address)
                .ToListAsync();

            var response = restaurants.Select(r => new RestaurantSummaryResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                PhoneNumber = r.PhoneNumber,
                Address = new AddressDto
                {
                    StreetAndNumber = r.Address.StreetAndNumber,
                    City = r.Address.City,
                    PostalCode = r.Address.PostalCode
                },
                Owner = new OwnerDto
                {
                    Id = r.Owner.Id,
                    UserId = r.Owner.UserId,
                    FirstName = r.Owner.User.FirstName,
                    LastName = r.Owner.User.LastName
                }
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
                PhoneNumber = restaurant.PhoneNumber,
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
                Description = "Popuni",
                PhoneNumber = "Popuni",
                Address = new Address()
                {
                    StreetAndNumber = "Popuni",
                    City = "Popuni",
                    PostalCode = "Popuni"
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
                PhoneNumber = restaurantUpdated.PhoneNumber,
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
            restaurant.PhoneNumber = updateRequest.PhoneNumber;
            restaurant.Address.StreetAndNumber = updateRequest.Address.StreetAndNumber;
            restaurant.Address.City = updateRequest.Address.City;
            restaurant.Address.PostalCode = updateRequest.Address.PostalCode;

            await _dbContext.SaveChangesAsync();

            var response = new RestaurantDetailResponse()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                PhoneNumber = restaurant.PhoneNumber,
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

        [HttpGet("{id}/menu")] //!!!!!!!!!!!!!!!!!!!!!OVO TREBA PODELITI NA VISE MANJIH METODA!!!!!!!!!!!!!!!!!!!!!!!!
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
                PhoneNumber = restaurant.PhoneNumber,
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

    }
}
