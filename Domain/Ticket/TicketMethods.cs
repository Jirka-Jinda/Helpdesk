using Domain.Ticket.TicketHistory;

namespace Domain.Ticket;

public partial class Ticket
{
    #region WF

    public bool CreateStateTransition(WFAction newAction)
    {
        if (WFTransitionRules.StateActions(WFState).Contains(newAction))
        {
            var newChange = new TicketChange(
                WFTransitionRules.ActionResolutions(WFState, newAction),
                newAction,
                TicketChanges
                );

            WFState = newChange.State;
            TicketChanges = newChange;

            return true;
        }
        return false;
    }

    public bool CreateSolverTransition(User solver, string description = "", TicketChange? change = null)
    {
        var newSolver = new SolverChange(
            solver, 
            description,
            SolverChanges
        );

        SolverChanges = newSolver;

        return true;
    }

    #endregion
}
