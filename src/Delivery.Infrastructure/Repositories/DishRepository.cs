using Delivery.Domain.Common;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class DishRepository : GenericRepository<Dish>, IDishRepository
{
    public DishRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Dish>> GetByIdsWithAllergensAsync(IEnumerable<Guid> dishIds)
    {
        return await _dbContext.Dishes
            .Where(d => dishIds.Contains(d.Id))
            .Include(d => d.Allergens)
            .ToListAsync();
    }

    public new async Task<IEnumerable<Dish>> GetAllAsync()
    {
        return await _dbContext.Dishes
            .Include(d => d.DishOptionGroups)
                .ThenInclude(g => g.DishOptions) // Include sve dish options
            .Include(d => d.Allergens) // ako želiš i alergene
            .ToListAsync();
    }

    public async Task<PaginatedList<Dish>> GetPagedAsync(int sort, DishFiltersMix filterMix, int page)
    {
        int pageSize = 6;

        IQueryable<Dish> query = _dbContext.Dishes
            .Include(d => d.Allergens);
        query = ApplyFilters(query, filterMix);
        query = ApplySorting(query, sort);

        var count = await query.CountAsync();
        var dishes = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        PaginatedList<Dish> result = new PaginatedList<Dish>(dishes, page, pageSize, count);
        return result;
    }

    public new async Task<Dish?> GetOneAsync(Guid id)
    {
        return await _dbContext.Dishes
            .Include(d => d.DishOptionGroups)
                .ThenInclude(g => g.DishOptions)
            .Include(d => d.Allergens)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public new async Task<Menu?> GetMenuAsync(Guid menuId)
    {
        return await _dbContext.Menus
            .Include(m => m.Dishes)
                .ThenInclude(d => d.Allergens)
            .Include(m => m.Dishes)
                .ThenInclude(d => d.DishOptionGroups)
                    .ThenInclude(g => g.DishOptions)
            .FirstOrDefaultAsync(m => m.Id == menuId);
    }

    private IQueryable<Dish> ApplySorting(IQueryable<Dish> query, int sort)
    {
        return sort switch
        {
            (int)DishSortTypes.NAME_DESC => query.OrderByDescending(d => d.Name),
            (int)DishSortTypes.TYPE_ASC => query.OrderBy(d => d.Type),
            (int)DishSortTypes.TYPE_DESC => query.OrderByDescending(d => d.Type),
            (int)DishSortTypes.PRICE_ASC => query.OrderBy(d => d.Price),
            (int)DishSortTypes.PRICE_DESC => query.OrderByDescending(d => d.Price),
            _ => query.OrderBy(d => d.Name)
        };
    }

    private IQueryable<Dish> ApplyFilters(IQueryable<Dish> dishes, DishFiltersMix filterMix)
    {
        if (!string.IsNullOrEmpty(filterMix.Name))
        {
            dishes = dishes.Where(r => r.Name.ToLower().Contains(filterMix.Name.ToLower()));
        }

        if (!string.IsNullOrEmpty(filterMix.Type))
        {
            dishes = dishes.Where(r => r.Type.ToLower().Contains(filterMix.Type.ToLower()));
        }

        if (filterMix.MinPrice.HasValue && filterMix.MaxPrice.HasValue)
        {
            dishes = dishes.Where(r => r.Price >= filterMix.MinPrice.Value && r.Price <= filterMix.MaxPrice.Value);
        }

        if (!filterMix.AllergicOnAlso && filterMix.Allergens != null && filterMix.Allergens.Count > 0)
        {
            dishes = dishes.Where(d =>
            !_dbContext.Set<Dictionary<string, object>>("AllergenDish")
                .Where(ad => filterMix.Allergens.Contains((Guid)ad["AllergensId"]))
                .Any(ad => (Guid)ad["DishesId"] == d.Id)
            );
        }

        return dishes;
    }
}
