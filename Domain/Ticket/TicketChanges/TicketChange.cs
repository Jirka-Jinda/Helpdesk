using Domain.Abstraction;

namespace Domain.Ticket.TicketChanges;

public class TicketChange : DomainObject
{
    public WFState State { get; init; }
    public WFAction Action { get; init; }
    public User.User Author { get; set; }
    public string? Description { get; set; }

    private TicketChange() { }

    public TicketChange(WFState changeState, WFAction changeAction)
    {
        State = changeState;
        Action = changeAction;
    }
}
