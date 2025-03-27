using Domain.Ticket.TicketHistory;
using Domain.Workflow.Enums;
using static Tests.DomainTests.DataObjects;

namespace Tests.DomainTests.TicketTests;

public class TicketTests
{
    public TicketTests()
    {

    }

    [Fact]
    public void Create_ticket_and_all_property_objects()
	{
        var ticket = emptyTicket;

        ticket.Thread.AddMessage("new text");

        if (ticket.CreateStateTransition(WFAction.Založení))
            Console.WriteLine("Zalozeno");

        Assert.Equal(WFState.Založený, ticket.WFState);
    }

    [Theory]
    [InlineData(new[] { WFAction.Založení, WFAction.Do_řešení, WFAction.Přidělení_ručně ,WFAction.Vyřešení })]
    public void Transition_valid_ticket_paths(WFAction[] actions)
    {
        var ticket = emptyTicket;

        foreach(var action in actions)
            Assert.True(ticket.CreateStateTransition(action), $"Invalid transition in test, from {ticket.WFState} using action {action}");

        int transitionCount = 0;
        TicketChange? prevChange = ticket.TicketChanges;
        while (prevChange != null)
        {
            Assert.InRange(transitionCount, 0, 50);
            transitionCount++;
            prevChange = prevChange.PreviousTransition;
        }

        Assert.Equal(actions.Length, transitionCount);
    }
}
