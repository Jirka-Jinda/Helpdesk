using Database.Repositories.Abstraction;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementation;

public class UserRepository(HelpdeskDbContext context) : IUserRepository
{
    private HelpdeskDbContext _context = context;

    public async Task<User> CreateAsync(User entity)
    {
        var entityEntry = await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task DeleteAsync(Guid Id)
    {
        var entity = await _context.Users.Where(t => t.Id == Id.ToString()).FirstOrDefaultAsync();
        if (entity is not null)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        var collection = await _context.Users
            .UseUserIncludesSingle()
            .ToListAsync();
        return collection;
    }

    public async Task<User?> GetAsync(Guid Id)
    {
        var entityEntry = await _context.Users
            .UseUserIncludesSingle()
            .Where(u => u.Id == Id.ToString())
            .FirstOrDefaultAsync();
        return entityEntry;
    }

    public async Task<ICollection<User>> GetByNameAsync(string name)
    {
        var collection = await _context.Users
            .UseUserIncludesCollection()
            .Where(u => u.UserName == name)
            .ToListAsync();
        return collection;
    }

    public async Task<ICollection<User>> GetByTypeAsync(UserType userType)
    {
        var collection = await _context.Users
            .UseUserIncludesCollection()
            .Where(u => u.UserType == userType)
            .ToListAsync();
        return collection;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        var entityEntry = _context.Users.Update(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }
}

file static class Extension
{
    public static IQueryable<User> UseUserIncludesSingle(this DbSet<User> users)
    {
        return users;
    }

    public static IQueryable<User> UseUserIncludesCollection(this DbSet<User> users)
    {
        return users;
    }
}

