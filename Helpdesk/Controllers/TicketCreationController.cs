using Helpdesk.Models.EventPlanner.Planner;

namespace Helpdesk.Controllers
{
    public class TicketCreationController : Controller
    {
        private readonly IEventPlannerManager eventPlannerManager;

        public TicketCreationController(IEventPlannerManager epm)
        {
            eventPlannerManager = epm;
        }

        public IActionResult Index()
        {
            eventPlannerManager.ScheduleTimer(new EventTimer(TimeSpan.FromSeconds(6), () => Console.WriteLine("Timer ticked"), true));            
            return View();
        }

        [Navigation(NavigationName = "TestName")]
        public IActionResult Test()
        {
            return View("Index");
        }
    }
}
