using Microsoft.EntityFrameworkCore;
using UserProfileService.Persistence;

namespace UserProfileService.Interfaces.Repositories;

public class UserProfileRepository<T>(ApplicationDbContext context) : IUserProfileRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public IQueryable GetQueryable() => _dbSet.AsQueryable();

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        await _dbSet.AddAsync(entity, cancellationToken);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}
