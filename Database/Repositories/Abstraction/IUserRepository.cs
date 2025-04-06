using Domain.User;

namespace Database.Repositories.Abstraction;

public interface IUserRepository
{
    public Task<User> CreateAsync(User entity);
    public Task<User?> GetAsync(Guid Id);
    public Task<ICollection<User>> GetAllAsync();
    public Task<ICollection<User>> GetByTypeAsync(UserType userType);
    public Task<ICollection<User>> GetByNameAsync(string name);
    public Task<User> UpdateAsync(User entity);
    public Task DeleteAsync(Guid Id);
}
