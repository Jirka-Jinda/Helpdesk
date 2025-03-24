using Domain.Abstraction;
using Domain.Messaging;
using Domain.Ticket.TicketHistory;

namespace Domain.Ticket;

public partial class Ticket : BaseDomainObject
{
    public Ticket Hierarchy { get; set; }
    public TicketType Type { get; set; }
    public TicketChange? TicketChanges { get; set; }
    public MessageThread Thread { get; set; }
    public WFState WFState { get; set; }
    public SolverChange? SolverChanges { get; set; }
    public TicketData Data { get; set; }
    
    public Ticket()
    {
        Hierarchy = this;
        WFState = WFState.Žádný;
        Thread = new MessageThread();
        TicketChanges = null;
    }
}