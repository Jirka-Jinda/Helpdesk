namespace Domain.Ticket;

public partial class Ticket
{
    #region WF

    public bool Transition(WFAction newAction, User author)
    {
        if (WFTransitionRules.StateActions(WFState).Contains(newAction))
        {
            var newChange = new TicketChange(
                WFTransitionRules.ActionResolutions(WFState, newAction),
                newAction,
                PreviousTransition
                );
                
            PreviousTransition = newChange;

            return true;
        }
        return false;
    }

    #endregion
}
