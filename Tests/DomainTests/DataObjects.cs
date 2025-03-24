using Domain.Ticket;

namespace Tests.DomainTests;

internal static class DataObjects
{
    public static Domain.Ticket.Ticket emptyTicket = new();

    public static Domain.Ticket.Ticket filledTicket = new()
    {
        Data = new TicketData()
        {
            Header = "Ticket Header",
            Description = "Big and long ticket description to be ideally trimmed and not shown whole"
        },
        SolverChanges = new Domain.Users.User() { Name = "Adolf Bily", UserType = Domain.Users.UserType.Řešitel},
        TimeCreated = DateTime.Now,        
    };

}
