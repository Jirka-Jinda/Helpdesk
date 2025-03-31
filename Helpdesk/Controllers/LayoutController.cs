using Database;
using Domain.User;
using Helpdesk.Models.Cookies;

namespace Helpdesk.Controllers
{
    public class LayoutController : Controller
	{
		private IStorageManager storageManager;
        private ILogger<LayoutController> logger;
        private HelpdeskDbContext _context;        

        public LayoutController(IStorageManager sm, ILogger<LayoutController> lg, HelpdeskDbContext context)
        {
            logger = lg;
            storageManager = sm;            
            _context = context;

            //DbDataBuilder dataBuilder = new(context);
            //dataBuilder.PopulateUsers().PopulateTickets();
            // docker run --name HelpdeskPostgres -p 32668:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=0iZZkGCfP4slqqd -d postgres:latest
        }

        public IActionResult Index()
		{
            logger.LogInformation("Index page was requested.");
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
        public IActionResult Users()
		{
            var user = new User()
            {
                UserName = "Adolf Bily",
                Email = "email@domain.cz",
                UserType = UserType.Řešitel
            };

            return View(user);
		}

        [Navigation(IgnoreMove = true)]
        public IActionResult Settings()
		{


			return View();
		}
    }
}
