namespace Domain.Ticket;

public partial class Ticket : BaseDomainObject
{
    public Ticket Hierarchy { get; set; }
    public TicketType Type { get; set; }
    public TicketChange PreviousTransition { get; set; }
    public TicketThread Thread { get; set; }
    public WFState WFState { get; set; }
    public TicketData Data { get; set; }

    public Ticket(TicketType type, TicketData data)
    {
        TimeCreated = DateTime.Now;
        Hierarchy = this;
        Type = type;
        PreviousTransition = WFTransition.Transition(WFState.Žádný, WFAction.Založení);
        Thread = new TicketThread();
        WFState = PreviousTransition.ChangeState;
        Data = data;
    }
}
