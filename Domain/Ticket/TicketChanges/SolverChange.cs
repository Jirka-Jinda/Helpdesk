using Domain.Abstraction;

namespace Domain.Ticket.TicketChanges;

public class SolverChange : DomainObject
{
    public TicketChange? TicketTransition { get; set; }
    public User.User Solver { get; set; }
    public string? Description { get; set; }

    private SolverChange() { }

    public SolverChange(User.User Solver, string Description)
    {
        this.Solver = Solver;
        this.Description = Description;
    }
}
