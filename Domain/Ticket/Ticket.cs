using Domain.Abstraction;
using Domain.Messaging;
using Domain.Ticket.TicketChanges;

namespace Domain.Ticket;

public partial class Ticket : DomainObject
{
    public Ticket Hierarchy { get; set; }
    public TicketType Type { get; set; }
    public List<TicketChange> TicketChanges { get; set; }
    public MessageThread Thread { get; set; }
    public WFState WFState { get; set; }
    public List<SolverChange> SolverChanges { get; set; }
    public User.User? Solver { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }

    public Ticket()
    {
        Hierarchy = this;
        WFState = WFState.Žádný;
        Thread = new MessageThread();
        TicketChanges = [];
        SolverChanges = [];
    }
}