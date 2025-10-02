using Delivery.Api.Contracts.Allergens;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class AllergensController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllergensController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allergens = await _context.Allergens
                .Select(a => new AllergenResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = a.Type
                })
                .ToListAsync();

            return Ok(allergens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var allergen = await _context.Allergens.FindAsync(id);

            if (allergen == null)
            {
                return NotFound();
            }

            var response = new AllergenResponse
            {
                Id = allergen.Id,
                Name = allergen.Name,
                Type = allergen.Type
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AllergenCreateRequest request)
        {
            var newAllergen = new Allergen
            {
                Name = request.Name,
                Type = request.Type
            };

            _context.Allergens.Add(newAllergen);
            await _context.SaveChangesAsync();

            var response = new AllergenResponse
            {
                Id = newAllergen.Id,
                Name = newAllergen.Name,
                Type = newAllergen.Type
            };

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AllergenUpdateRequest request)
        {
            var allergen = await _context.Allergens.FindAsync(id);

            if (allergen == null)
            {
                return NotFound();
            }

            allergen.Name = request.Name;
            allergen.Type = request.Type;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var allergen = await _context.Allergens.FindAsync(id);

            if (allergen == null)
            {
                return NotFound();
            }

            _context.Allergens.Remove(allergen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
