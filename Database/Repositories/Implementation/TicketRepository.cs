using Database.Repositories.Abstraction;
using Domain.Ticket;
using Domain.Workflow.Enums;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementation;

public class TicketRepository(HelpdeskDbContext context) : ITicketRepository
{
    private readonly HelpdeskDbContext _context = context;
    //private readonly UserService _userService = userService;

    public async Task<Ticket> CreateAsync(Ticket entity)
    {
        entity.TimeCreated = entity.TimeLastModified = DateTime.UtcNow;
        //entity.UserCreated = entity.UserLastModified = _userService.GetCurrentUser();

        var entityEntry = await _context.Tickets.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task DeleteAsync(Guid Id)
    {
        var entity = _context.Tickets.Where(t => t.Id == Id).FirstOrDefault();
        if (entity is not null)
        {
            _context.Tickets.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Ticket?> GetAsync(Guid Id)
    {
        var entity = await _context.Tickets
            .UseTicketIncludesSingle()
            .SingleOrDefaultAsync(t => t.Id == Id);
        return entity;
    }

    public async Task<ICollection<Ticket>> GetByCreatorAsync(Guid userId)
    {
        var collection = await _context.Tickets
            .UseTicketIncludesCollection()
            .Where(t => t.UserCreated.Id == userId.ToString())
            .ToListAsync();
        return collection;            
    }

    public async Task<ICollection<Ticket>> GetBySolverAsync(Guid userId)
    {
        var collection = await _context.Tickets
            .UseTicketIncludesCollection()
            .Where(t => t.Solver.Id == userId.ToString())
            .ToListAsync();
        return collection;
    }

    public async Task<ICollection<Ticket>> GetByStateAsync(WFState wfState)
    {
        var collection = await _context.Tickets
            .UseTicketIncludesCollection()
            .Where(t => t.WFState == wfState)
            .ToListAsync();
        return collection;
    }

    public async Task<ICollection<Ticket>> GetAllAsync()
    {
        var collection = await _context.Tickets
            .UseTicketIncludesCollection()
            .ToListAsync();
        return collection;
    }

    public async Task<Ticket> UpdateAsync(Ticket entity)
    {
        var entityEntry = _context.Update(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }
}

file static class Extension
{
    public static IQueryable<Ticket> UseTicketIncludesSingle(this DbSet<Ticket> tickets)
    {
        return tickets
            .Include(t => t.Thread)
            .ThenInclude(t => t.Messages)
            .Include(t => t.TicketChanges)
            .Include(t => t.SolverChanges)
            .Include(t => t.Solver);
        
    }

    public static IQueryable<Ticket> UseTicketIncludesCollection(this DbSet<Ticket> tickets)
    {
        return tickets
            .Include(t => t.UserCreated);
    }
}
