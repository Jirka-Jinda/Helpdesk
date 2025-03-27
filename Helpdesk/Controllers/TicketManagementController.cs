using Database;
using Domain.User;
using Domain.Workflow.Enums;

namespace Helpdesk.Controllers
{
    public class TicketManagementController : Controller
    {
        private IStorageManager _storageManager;
        private HelpdeskDbContext _context;

        // Delete after testing
        public static Ticket filledTicket = new()
        {
            Header = "Ticket Header",
            Description = "Big and long ticket description to be ideally trimmed and not shown whole",
            SolverChanges = new(
                new Domain.User.User()
                {
                    UserName = "Adolf Bily",
                    UserType = Domain.User.UserType.Řešitel
                },
                "solver changed",
                null
            )
        };     
        //

        public TicketManagementController(IStorageManager storageManager, HelpdeskDbContext context)
        {
            _storageManager = storageManager;
            _context = context;            
        }

        public IActionResult Overview()
        {   
            _context.Tickets.Add(filledTicket);
            _context.SaveChanges();

            // Create cash, then retrieve one needed
            var allTickets = _context.Tickets.ToList();

            return View(allTickets);
        }

        [Navigation(NavigationName = "Detail")]
        public IActionResult Detail(Guid ticketId)
        {
            var ticket = _context.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();

            return View(ticket);
        }

        [Navigation(IgnoreMove = true)]
        public IActionResult PostMessage(Guid ticketId, string content)
        {
            var ticket = _context.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();
            ticket?.Thread.AddMessage(content);
            _context.SaveChanges();

            return View("Detail", ticket);
        }

        [Navigation(IgnoreMove = true)]
        public IActionResult ChangeWF(Guid ticketId, WFAction action)
        {
            filledTicket.CreateStateTransition(action);

            return RedirectToAction("Detail");
        }

        [Navigation(IgnoreMove = true)]
        public IActionResult ChangeSolver(Guid ticketId, User newSolver)
        {
            filledTicket.CreateSolverTransition(newSolver, "", null);

            return RedirectToAction("Detail");
        }
    }
}
