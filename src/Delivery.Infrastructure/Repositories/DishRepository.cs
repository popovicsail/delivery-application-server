using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class DishRepository : GenericRepository<Dish>, IDishRepository
{
    public DishRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
