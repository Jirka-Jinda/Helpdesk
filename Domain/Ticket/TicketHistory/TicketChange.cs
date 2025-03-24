using Domain.Abstraction;

namespace Domain.Ticket.TicketHistory;

public class TicketChange : BaseDomainObject
{
    public TicketChange? PreviousTransition { get; init; }
    public WFState State { get; init; }
    public WFAction Action { get; init; }
    public User Author { get; set; }
    public string Description { get; set; }

    public TicketChange(WFState changeState, WFAction changeAction, TicketChange? previousChange)
    {
        State = changeState;
        Action = changeAction;
        PreviousTransition = previousChange;
    }
}
