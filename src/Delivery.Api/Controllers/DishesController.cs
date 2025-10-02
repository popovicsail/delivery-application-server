using Delivery.Api.Contracts.Dishes;
using Delivery.Api.Contracts.Helper;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DishesController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        
        public DishesController (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dishes = await _dbContext.Dishes.Select(d => new DishSummaryResponse
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                Description = d.Description,
                Type = d.Type
            }).ToListAsync();

            return Ok(dishes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var dish = await _dbContext.Dishes
                .Include(d => d.Menu)
                .Include(d => d.DishOptionGroups)
                    .ThenInclude(dog => dog.DishOptions)
                .Include(d => d.Allergens)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            var response = new DishDetailResponse
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Type = dish.Type,
                MenuId = dish.Menu.Id,
                DishOptionGroups = dish.DishOptionGroups.Select(dog => new DishOptionGroupDto
                {
                    Id = dog.Id,
                    Name = dog.Name,
                    DishOptions = dog.DishOptions.Select(dopt => new DishOptionDto
                    {
                        Id = dopt.Id,
                        Name = dopt.Name,
                        Price = dopt.Price
                    }).ToList()

                }).ToList(),
                Allergens = dish.Allergens.Select(a => new AllergenDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList()

            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DishCreateRequest request)
        {
            var newDish = new Dish
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Type = request.Type,
                MenuId = request.MenuId
            };

            _dbContext.Dishes.Add(newDish);

            await _dbContext.SaveChangesAsync();

            return Ok(newDish);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] DishUpdateRequest request)
        {
            var dish = await _dbContext.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Price = request.Price;
            dish.Type = request.Type;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var dish = await _dbContext.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            _dbContext.Dishes.Remove(dish);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
