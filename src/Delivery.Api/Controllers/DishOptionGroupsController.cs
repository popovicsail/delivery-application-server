using Delivery.Api.Contracts.Dishes;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Delivery.Api.Contracts.Helper;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DishOptionGroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DishOptionGroupsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DishOptionGroupCreateRequest request)
        {
            var dish = await _dbContext.Dishes.FirstOrDefaultAsync(d => d.Id == request.DishId);

            var newGroup = new DishOptionGroup
            {
                Name = request.Name,
                Dish = dish,
                Type = request.Type
            };

            foreach (var optionDto in request.DishOptions)
            {
                var newOption = new DishOption
                {
                    Name = optionDto.Name,
                    Price = optionDto.Price,
                    DishOptionGroup = newGroup
                };
                newGroup.DishOptions.Add(newOption);
            }

            _dbContext.DishOptionGroups.Add(newGroup);

            await _dbContext.SaveChangesAsync();

            return Ok(); //Treba mapirati na DTO i vratiti
        }
    }
}
