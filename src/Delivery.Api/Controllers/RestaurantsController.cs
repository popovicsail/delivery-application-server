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
                OwnerId = createRequest.OwnerId,
                BaseWorkSched = new BaseWorkSched()
                {
                    Saturday = true,
                    Sunday = true,
                    WorkDayStart = new TimeSpan(8, 0, 0),
                    WorkDayEnd = new TimeSpan(17, 0, 0),
                    WeekendStart = new TimeSpan(10, 0, 0),
                    WeekendEnd = new TimeSpan(16, 0, 0)
                }
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

        [HttpGet("my-restaurants")]
        public async Task<IActionResult> GetMyRestaurants()
        {
            var user = await _userManager.GetUserAsync(User);

            var owner = await _dbContext.Owners
                .FirstOrDefaultAsync(o => o.UserId == user.Id);

            var restaurants = await _dbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.BaseWorkSched)
                .Where(r => r.OwnerId == owner.Id)
                .Select(r => new RestaurantSummaryResponse
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
                    Id = owner.Id,
                    UserId = owner.UserId,
                    FirstName = owner.User.FirstName,
                    LastName = owner.User.LastName
                },
                BaseWorkSched = new BaseWorkSchedDto
                {
                    Id = r.BaseWorkSched.Id,
                    Saturday = r.BaseWorkSched.Saturday,
                    Sunday = r.BaseWorkSched.Sunday,
                    WorkDayStart = r.BaseWorkSched.WorkDayStart,
                    WorkDayEnd = r.BaseWorkSched.WorkDayEnd,
                    WeekendStart = r.BaseWorkSched.WeekendStart,
                    WeekendEnd = r.BaseWorkSched.WeekendEnd
                }
                }).ToListAsync();

            return Ok(restaurants);
        }
    }

}

