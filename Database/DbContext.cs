using Domain.Messaging;
using Domain.Ticket;
using Domain.Ticket.TicketHistory;
using Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class HelpdeskDbContext : IdentityDbContext<User>
{
    DbSet<Ticket> Tickets { get; set; }
    DbSet<TicketChange> TicketChanges { get; set; }
    DbSet<SolverChange> SolverChanges { get; set; }    
    DbSet<MessageThread> Threads { get; set; }
    DbSet<Message> Messages { get; set; }
    
    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options)
    {

    }
}
