namespace Domain.Ticket;

public class TicketChange : BaseDomainObject
{
    public TicketChange? PreviousTransition { get; init; }
    public WFState ChangeState { get; init; }
    public WFAction ChangeAction { get; init; }
    public User ChangeAuthor { get; set; }
    public string Description { get; set; }

    public TicketChange(WFState changeState, WFAction changeAction, TicketChange? previousChange)
    {
        ChangeState = changeState;
        ChangeAction = changeAction;
        PreviousTransition = previousChange;
    }
}
