using Domain.Messaging;
using Domain.Ticket;
using Domain.Users;

namespace EntityFramework;

public class HelpdeskDbContext : DbContext
{
    DbSet<Ticket> Tickets { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<User> Users { get; set; }
}
