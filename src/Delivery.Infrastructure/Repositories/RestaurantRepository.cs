using Delivery.Domain.Common;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {
    }

    public new async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _dbContext.Restaurants
            .Include(r => r.Owner)
                .ThenInclude(o => o.User)
            .Include(r => r.Address)
            .Include(r => r.BaseWorkSched)
            .ToListAsync();
        return restaurants;
    }

    public async Task<PaginatedList<Restaurant>> GetPagedAsync(int sort, RestaurantFiltersMix filterMix, int page)
    {
        int pageSize = 6;

        //var count = await _dbContext.Restaurants.CountAsync();

        IQueryable<Restaurant> query = _dbContext.Restaurants
            .Include(r => r.Owner)
                .ThenInclude(o => o.User)
            .Include(r => r.Address)
            .Include(r => r.BaseWorkSched);
        query = ApplyFilters(query, filterMix);
        query = ApplySorting(query, sort);

        var count = await query.CountAsync();
        var restaurants = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        PaginatedList<Restaurant> result = new PaginatedList<Restaurant>(restaurants, page, pageSize, count);
        return result;
    }

    public async Task<IEnumerable<Restaurant>> GetMyAsync(Guid ownerId)
    {
        var restaurants = await _dbContext.Restaurants
            .Include(r => r.Owner)
            .Include(r => r.Address)
            .Include(r => r.BaseWorkSched)
            .Where(r => r.OwnerId == ownerId)
            .ToListAsync();
        return restaurants;
    }

    public new async Task<Restaurant?> GetOneAsync(Guid id)
    {
        var restaurant = await _dbContext.Restaurants
            .Include(r => r.Owner)
            .Include(r => r.Address)
            .Include(r => r.BaseWorkSched)
            .Include(r => r.WorkSchedules)
            .Include(r => r.Menus)
            .Include(r => r.Workers)
            .FirstOrDefaultAsync(o => o.Id == id);

        return restaurant;
    }

    public async Task<Menu> GetMenuAsync(Guid id)
    {
        var menu = await _dbContext.Menus
            .Where(m => m.RestaurantId == id)
            .Include(m => m.Dishes)
                .ThenInclude(d => d.DishOptionGroups)
                    .ThenInclude(g => g.DishOptions)
            .Include(m => m.Dishes)
                .ThenInclude(d => d.Allergens)
            .FirstOrDefaultAsync();
        return menu!;
    }

    public async Task<IEnumerable<Worker>> GetWorkersAsync(Guid restaurantId)
    {
        var workers = await _dbContext.Workers
            .Include(w => w.User)
            .Where(w => w.RestaurantId == restaurantId)
            .ToListAsync();
        return workers;
    }

    private static IQueryable<Restaurant> ApplySorting(IQueryable<Restaurant> query, int sort)
    {
        return sort switch
        {
            (int)RestaurantSortTypes.NAME_DESC => query.OrderByDescending(r => r.Name),
            _ => query.OrderBy(r => r.Name)
        };
    }

    private static IQueryable<Restaurant> ApplyFilters(IQueryable<Restaurant> restaurants, RestaurantFiltersMix filterMix)
    {
        if (!string.IsNullOrEmpty(filterMix.Name))
        {
            restaurants = restaurants.Where(r => r.Name.ToLower().Contains(filterMix.Name.ToLower()));
        }

        if (!string.IsNullOrEmpty(filterMix.City))
        {
            restaurants = restaurants.Where(r => r.Address.City.ToLower().Contains(filterMix.City.ToLower()));
        }


        bool fullDay = filterMix.OpeningTime <= TimeSpan.Zero && filterMix.ClosingTime >= new TimeSpan(23, 59, 0);
        DateTime today = DateTime.UtcNow;

        if (!fullDay || (fullDay && !filterMix.ClosedToo)) //Apply if user set "from" and "to" times or if user doesn't want closed restaurants in full day filter
        {
            if ((int)today.DayOfWeek == 0)
            {
                restaurants = restaurants.Where(r => (r.BaseWorkSched.Sunday));
            }
            else if ((int)today.DayOfWeek == 6)
            {
                restaurants = restaurants.Where(r => (r.BaseWorkSched.Saturday));
            }
        }

        if ((int)today.DayOfWeek < 6 && !fullDay)
        {
            restaurants = restaurants.Where(r =>
            (
              // Case 1: restaurant oepn time doesn't cross midnight (e.g. 08→16)
              (r.BaseWorkSched.WorkDayStart < r.BaseWorkSched.WorkDayEnd &&
            
                filterMix.OpeningTime < filterMix.ClosingTime &&
                r.BaseWorkSched.WorkDayStart < filterMix.ClosingTime &&
                r.BaseWorkSched.WorkDayEnd > filterMix.OpeningTime)
              ||
              // Case 2: restaurant open time crosses midnight (e.g. 22→06)
              (r.BaseWorkSched.WorkDayStart > r.BaseWorkSched.WorkDayEnd &&
                // I'll come somewhat after midnight
                (filterMix.OpeningTime < r.BaseWorkSched.WorkDayEnd ||
                 // I'll go home somewhat before midnight
                 filterMix.ClosingTime > r.BaseWorkSched.WorkDayStart
                 ))
              ||
              // Case 3: user’s range crosses midnight (e.g. 23→03)
              (filterMix.OpeningTime > filterMix.ClosingTime &&
                // Restaurant opens after midnight and I still am able to spend time there
                (r.BaseWorkSched.WorkDayStart < filterMix.ClosingTime ||
                 // Restaurant closes before midnight and I can come earlier than they close
                 r.BaseWorkSched.WorkDayEnd > filterMix.OpeningTime))
            ));
        }
        else if ((int)today.DayOfWeek == 6 || (int)today.DayOfWeek == 0 && !fullDay)
        {
            restaurants = restaurants.Where(r =>
            (
              r.BaseWorkSched.Saturday && (
              // Case 1: restaurant oepn time doesn't cross midnight (e.g. 08→16)
              (r.BaseWorkSched.WeekendStart < r.BaseWorkSched.WeekendEnd &&
              
                filterMix.OpeningTime < filterMix.ClosingTime &&
                r.BaseWorkSched.WeekendStart < filterMix.ClosingTime &&
                r.BaseWorkSched.WeekendEnd > filterMix.OpeningTime)
              ||
              // Case 2: restaurant open time crosses midnight (e.g. 22→06)
              (r.BaseWorkSched.WeekendStart > r.BaseWorkSched.WeekendEnd &&
                // I'll come somewhat after midnight
                (filterMix.OpeningTime < r.BaseWorkSched.WeekendEnd ||
                // I'll go home somewhat before midnight
                filterMix.ClosingTime > r.BaseWorkSched.WeekendStart))
              ||
              // Case 3: user’s range crosses midnight (e.g. 23→03)
              (filterMix.OpeningTime > filterMix.ClosingTime &&
                // Restaurant opens after midnight and I still am able to spend time there
                (r.BaseWorkSched.WeekendStart < filterMix.ClosingTime ||
                // Restaurant closes before midnight and I can come earlier than they close
                r.BaseWorkSched.WeekendEnd > filterMix.OpeningTime))
            )));
        }

        if (filterMix.ClosedToo == false && fullDay) //Closed too can only work if user wants full day filter
        {
            //Moglo bi da se doda rightNow + x minuta ukoliko restoran ne zeli da primi dostave x minuta pre zatvaranja
            TimeSpan rightNow = today.TimeOfDay;
            if ((int)today.DayOfWeek < 6 && (int)today.DayOfWeek > 0)
            {
                restaurants = restaurants.Where(r => (
                r.BaseWorkSched.WorkDayStart < r.BaseWorkSched.WorkDayEnd &&
                (
                    (r.BaseWorkSched.WorkDayStart <= rightNow) && (r.BaseWorkSched.WorkDayEnd > rightNow))
                ||
                r.BaseWorkSched.WorkDayStart > r.BaseWorkSched.WorkDayEnd &&
                (
                (r.BaseWorkSched.WorkDayStart <= rightNow) || (r.BaseWorkSched.WorkDayEnd > rightNow))
                ));
            }
            else if ((int)today.DayOfWeek == 6 || (int)today.DayOfWeek == 0)
            {
                restaurants = restaurants.Where(r => (
                r.BaseWorkSched.WorkDayStart < r.BaseWorkSched.WorkDayEnd &&
                (
                    (r.BaseWorkSched.WeekendStart <= rightNow) && (r.BaseWorkSched.WeekendEnd > rightNow))
                ||
                r.BaseWorkSched.WeekendStart > r.BaseWorkSched.WeekendEnd &&
                (
                (r.BaseWorkSched.WeekendStart <= rightNow) || (r.BaseWorkSched.WeekendEnd > rightNow))
                ));
            }
        }

        return restaurants;
    }
}
