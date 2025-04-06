using Domain.Ticket.TicketChanges;
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
                newAction
                );
            newChange.TimeCreated = DateTime.UtcNow;
            WFState = newChange.State;
            TicketChanges.Add(newChange);

            return true;
        }
        return false;
    }

    public bool CreateSolverTransition(User.User solver, string description = "", TicketChange? change = null)
    {
        var newSolver = new SolverChange(
            solver,
            description
        );
        newSolver.TimeCreated = DateTime.UtcNow;
        if (change is not null)
            newSolver.TicketTransition = change;

        Solver = newSolver.Solver;
        SolverChanges.Add(newSolver);

        return true;
    }

    #endregion

    public void AddMessage(string message)
    {
        Thread.Messages.Add(new Messaging.Message(message) { TimeCreated = DateTime.UtcNow });
    }
}
