namespace Domain.Ticket
{
    public class TicketChange
    {
        public TicketChange PreviousChange { get; set; }
        public WFState ChangeState { get; set; }
        public WFAction ChangeAction { get; set; }
        public User ChangeAuthor { get; set; }
        public DateTime ChangeTime { get; set; }
        public string Description { get; set; }
    }
}
