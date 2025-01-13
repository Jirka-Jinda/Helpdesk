using Domain.Ticket;

namespace Tests.DomainTests;

public class TicketTests
{
	public TicketTests()
	{

	}

	[Fact]
	public void CreateTicket_WhenValidInput_CreatesTicket()
	{
		// Arrange
		var ticket = new Ticket(TicketType.Základní, new TicketData() { Header = "Header", Description = "Description" });

		
	}
}
