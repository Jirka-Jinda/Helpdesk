namespace Domain.Abstraction.BaseInterfaces;

public interface IRepository<T> where T : BaseDomainObject
{
    Task<T> CreateAsync(T entity);
    Task<T> GetAsync(T Entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
