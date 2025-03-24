namespace Domain.Ticket.TicketHistory;

public class SolverChange : BaseDomainObject
{
    public SolverChange? PreviousTransition { get; init; }
    public TicketChange? TicketTransition { get; set; }
    public User Solver { get; set; }
    public string Description { get; set; }

    public SolverChange(User solver, string description, SolverChange? previousChange)
    {
        Solver = solver;
        Description = description;
        PreviousTransition = previousChange;
    }
}
