using Database;
using Domain.Messaging;
using Domain.Users;
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
            Data = new TicketData()
            {
                Header = "Ticket Header",
                Description = "Big and long ticket description to be ideally trimmed and not shown whole"
            },
            SolverChanges = new(
                new Domain.Users.User()
                {
                    UserName = "Adolf Bily",
                    UserType = Domain.Users.UserType.Řešitel
                },
                "solver changed",
                null
            ),
            TimeCreated = DateTime.Now,
        };     
        //

        public TicketManagementController(IStorageManager storageManager, HelpdeskDbContext context)
        {
            _storageManager = storageManager;
            _context = context;
        }

        public IActionResult Overview()
        {           

            var list = new List<Ticket>() { filledTicket };

            return View(list as IReadOnlyCollection<Ticket>);
        }

        [Navigation(NavigationName = $"Detail")]
        public IActionResult Detail(Guid ticketId)
        {
            Console.WriteLine(ticketId);

            return View(filledTicket);
        }

        [Navigation(IgnoreMove = true)]
        public IActionResult PostMessage(Guid ticketId, string content)
        {
            filledTicket.Thread.AddMessage(new MessageContent() { Text = content });

            return RedirectToAction("Detail");
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
