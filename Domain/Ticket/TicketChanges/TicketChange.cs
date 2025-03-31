using Domain.Abstraction;

namespace Domain.Ticket.TicketChanges;

public class TicketChange : DomainObject
{
    public TicketChange? PreviousTransition { get; init; }
    public WFState State { get; init; }
    public WFAction Action { get; init; }
    public User.User Author { get; set; }
    public string Description { get; set; }

    private TicketChange() { }

    public TicketChange(WFState changeState, WFAction changeAction, TicketChange? previousChange)
    {
        State = changeState;
        Action = changeAction;
        PreviousTransition = previousChange;
    }
}
