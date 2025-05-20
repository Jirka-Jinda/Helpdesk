using Database;
using Database.Repositories.Abstraction;
using Database.Repositories.Implementation;
using Domain.User;
using Helpdesk.Models.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Controllers
{
    public class LayoutController : Controller
	{
		private IStorageManager storageManager;
        private ILogger<LayoutController> logger;
        private HelpdeskDbContext _context;        
        private ITicketRepository _ticketRepository;
        private readonly UserManager<User> _userManager;


        public LayoutController(IStorageManager sm,
                                ILogger<LayoutController> lg,
                                HelpdeskDbContext context,
                                ITicketRepository ticketRepository,
                                UserManager<User> userManager)
        { 
            logger = lg;
            storageManager = sm;            
            _context = context;
            _ticketRepository = ticketRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
		{
            return View();
        }

        [Navigation(IgnoreMove = true)]
		public IActionResult Search(string search)
		{
            Console.WriteLine(search);

			return View("Index");
		}

        [Navigation(IgnoreMove = true)]
        public IActionResult SwitchTheme()
        {
			if (storageManager.GetScoped(out IUserSettings? userSettings))
			{
                userSettings?.SwitchTheme();
				storageManager.PutScoped(userSettings);
            }

            return View("Index");
        }

        [Navigation(IgnoreMove = true)]
        public async Task<IActionResult> Users()
		{
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
                return RedirectToAction("Login", "Access");

            return View(user);
        }

        [Navigation(IgnoreMove = true)]
        public IActionResult Settings()
		{
			return View();
		}
    }
}
