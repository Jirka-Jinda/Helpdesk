using System.Collections.ObjectModel;

namespace Domain.Workflow;

public static class WFTransitionRules
{
    public static ReadOnlyCollection<WFAction> StateActions(WFState state)
    {
        return state switch
        {
            WFState.Žádný => new ReadOnlyCollection<WFAction>([WFAction.Založení]),
            WFState.Založený => new ReadOnlyCollection<WFAction>([WFAction.Do_řešení]),
            WFState.Nepřidělený => new ReadOnlyCollection<WFAction>([WFAction.Přidělení_ručně, WFAction.Přidělení_timer, WFAction.Přidělení_manager]),
            WFState.V_řešení => new ReadOnlyCollection<WFAction>([WFAction.Odložení, WFAction.Vyřešení, WFAction.Žádost_o_potvrzení, WFAction.Žádost_o_spolupráci, WFAction.Žádost_o_vyjádření_zadavatele, WFAction.Změna_řešitele]),
            WFState.Neaktivní => new ReadOnlyCollection<WFAction>([WFAction.Reaktivace_automatická, WFAction.Reaktivace_ruční]),
            WFState.Uzavřený => new ReadOnlyCollection<WFAction>([WFAction.Vrácení]),
            WFState.Vrácený => new ReadOnlyCollection<WFAction>([WFAction.Vrácení]),
            _ => new ReadOnlyCollection<WFAction>([]),
        };
    }

    public static WFState ActionResolutions(WFState startState, WFAction action)
    {
        switch (startState)
        {
            case WFState.Žádný:
                if (action == WFAction.Založení) return WFState.Založený;
                break;
            case WFState.Založený:
                if (action == WFAction.Do_řešení) return WFState.Nepřidělený;
                break;
            case WFState.Nepřidělený:
                if (action == WFAction.Přidělení_ručně) return WFState.V_řešení;
                if (action == WFAction.Přidělení_timer) return WFState.V_řešení;
                if (action == WFAction.Přidělení_manager) return WFState.V_řešení;
                break;
            case WFState.V_řešení:
                if (action == WFAction.Odložení) return WFState.Neaktivní;
                if (action == WFAction.Vyřešení) return WFState.Uzavřený;
                if (action == WFAction.Žádost_o_potvrzení) return WFState.V_řešení;
                if (action == WFAction.Žádost_o_spolupráci) return WFState.V_řešení;
                if (action == WFAction.Žádost_o_vyjádření_zadavatele) return WFState.V_řešení;
                if (action == WFAction.Změna_řešitele) return WFState.Nepřidělený;
                break;
            case WFState.Neaktivní:
                if (action == WFAction.Reaktivace_automatická) return WFState.V_řešení;
                if (action == WFAction.Reaktivace_ruční) return WFState.V_řešení;
                break;
            case WFState.Uzavřený:
                if (action == WFAction.Vrácení) return WFState.Vrácený;
                break;
            case WFState.Vrácený:
                if (action == WFAction.Vrácení) return WFState.V_řešení;
                break;
        };
        return WFState.Neplatný;
    }    
}
