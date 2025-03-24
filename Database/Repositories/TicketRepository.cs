namespace Database.Repositories;

public class TicketRepository
{
    private HelpdeskDbContext _context;

    public TicketRepository(HelpdeskDbContext context)
    {
        _context = context;
    }


}
