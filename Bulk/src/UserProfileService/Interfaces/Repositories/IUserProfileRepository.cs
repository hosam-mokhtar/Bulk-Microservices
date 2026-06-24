namespace UserProfileService.Interfaces.Repositories;

public interface IUserProfileRepository<T> where T : class
{
    IQueryable GetQueryable();
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
