using Domain.Abstraction;

namespace Database.Repositories.Abstraction;

public interface IDbRepository<T> where T : DomainObject
{
    public Task<T> CreateAsync(T entity);
    public Task<T?> GetAsync(Guid Id);
    public Task<T> UpdateAsync(T entity);
    public Task DeleteAsync(Guid Id);
}
