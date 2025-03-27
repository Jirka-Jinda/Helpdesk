using Domain.Abstraction;

namespace Domain.Ticket.TicketHistory;

public class SolverChange : BaseDomainObject
{
    public SolverChange? PreviousTransition { get; init; }
    public TicketChange? TicketTransition { get; set; }
    public User.User Solver { get; set; }
    public string Description { get; set; }

    private SolverChange() { }

    public SolverChange(User.User Solver, string Description, SolverChange? PreviousTransition)
    {
        this.Solver = Solver;
        this.Description = Description;
        this.PreviousTransition = PreviousTransition;
    }
}
 