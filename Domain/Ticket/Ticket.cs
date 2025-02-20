using Domain.Messaging;

namespace Domain.Ticket;

public partial class Ticket : BaseDomainObject
{
    public Ticket Hierarchy { get; set; }
    public TicketType Type { get; set; }
    public TicketChange? PreviousTransition { get; set; }
    public MessageThread Thread { get; set; }
    public WFState WFState { get; set; }
    public User Author { get; set; }  
    public User Solver { get; set; }
    public TicketData Data { get; set; }
    
    public Ticket()
    {
        Hierarchy = this;
        WFState = WFState.Žádný;
        Thread = new MessageThread();
        PreviousTransition = null;
    }
}