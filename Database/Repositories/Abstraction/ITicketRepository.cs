using Domain.Ticket;
using Domain.Workflow.Enums;

namespace Database.Repositories.Abstraction;

public interface ITicketRepository : IDbRepository<Ticket>
{
    public Task<ICollection<Ticket>> GetByCreatorAsync(Guid userId);
    public Task<ICollection<Ticket>> GetBySolverAsync(Guid userId);
    public Task<ICollection<Ticket>> GetByStateAsync(WFState wfState);
    public Task<ICollection<Ticket>> GetAllAsync();
}
