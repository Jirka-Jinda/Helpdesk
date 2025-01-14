namespace Domain.Workflow.Transition;

public static class WFTransitionRules
{
    public static List<WFAction>? TransitionPath(WFState startState, WFState endState)
    {
        return TransitionPathfinder(startState, endState, new List<WFAction>());
    }

    private static List<WFAction>? TransitionPathfinder(WFState startState, WFState endState, List<WFAction> actions)
    {
        if (startState == endState)
            return actions;
        
        //automaton
    }

}
