using Domain.Ticket.TicketHistory;
using Domain.Workflow;

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

    public bool CreateSolverTransition(User.User solver, string description = "", TicketChange? change = null)
    {
        var newSolver = new SolverChange(
            solver, 
            description,
            SolverChanges
        );
        if (change is not null)
            newSolver.TicketTransition = change;

        SolverChanges = newSolver;

        return true;
    }

    #endregion
}
