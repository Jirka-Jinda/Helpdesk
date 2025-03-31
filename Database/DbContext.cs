using Domain.Messaging;
using Domain.Ticket;
using Domain.Ticket.TicketChanges;
using Domain.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class HelpdeskDbContext : IdentityDbContext<User>
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketChange> TicketChanges { get; set; }
    public DbSet<SolverChange> SolverChanges { get; set; }    
    public DbSet<MessageThread> Threads { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options)
    {

    }
}
