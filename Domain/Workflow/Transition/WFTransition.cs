namespace Domain.Workflow.Transition
{
    public class WFTransition
    {
        public static TicketChange Transition(WFState startState, WFAction action)
        {
            return new TicketChange();
        }
    }
}
