﻿using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }

    public new async Task<IEnumerable<Owner>> GetAllAsync()
    {
        var owners = await _dbContext.Owners
            .Include(o => o.User)
            .ToListAsync();

        return owners;
    }

    public new async Task<Owner?> GetOneAsync(Guid id)
    {
        var owner = await _dbContext.Owners
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == id);

        return owner;
    }

    public async Task<Owner?> GetByUserIdAsync(Guid userId)
    {
        var owner = await _dbContext.Owners
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.UserId == userId);
        return owner;
    }
}
