using System.Collections.ObjectModel;

namespace Domain.Workflow;

public static class WFTransitionRules
{
    public static ReadOnlyCollection<WFAction> StateActions(WFState state)
    {
        switch (state)
        {
            case WFState.Žádný:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Založení });
            case WFState.Založený:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Do_řešení });
            case WFState.Nepřidělený:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Přidělení_ručně, WFAction.Přidělení_timer, WFAction.Přidělení_manager });
            case WFState.V_řešení:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Odložení, WFAction.Vyřešení, WFAction.Žádost_o_potvrzení, WFAction.Žádost_o_spolupráci, WFAction.Žádost_o_vyjádření_zadavatele, WFAction.Změna_řešitele });
            case WFState.Neaktivní:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Reaktivace_automatická, WFAction.Reaktivace_ruční });                
            case WFState.Uzavřený:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Vrácení });
            case WFState.Vrácený:
                return new ReadOnlyCollection<WFAction>(new List<WFAction>() { WFAction.Vrácení });
        }
        return new ReadOnlyCollection<WFAction>(new List<WFAction>());
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
