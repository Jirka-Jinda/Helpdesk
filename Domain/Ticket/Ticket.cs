namespace Domain.Ticket
{
    public partial class Ticket : BaseDomainObject
    {
        public Ticket? Hierarchy { get; set; }
        public TicketType Type { get; set; }
        public TicketChange History { get; set; }
        public TicketThread Thread { get; set; }
        public WFState WFState { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }

        public Ticket(string header, string description, TicketType type, Ticket? hierarchy = null)
        {
            TimeCreated = DateTime.Now;
            Hierarchy = hierarchy;
            Type = type;
            History = WFTransition.Transition(WFState.Žádný, WFAction.Založení);
            Thread = new TicketThread();
            WFState = History.ChangeState;

            Header = header;
            Description = description;
        }
    }
}
