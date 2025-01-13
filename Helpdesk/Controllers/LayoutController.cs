using Helpdesk.Models.Cookies;

namespace Helpdesk.Controllers
{
    public class LayoutController : Controller
	{
		private IStorageManager storageManager;
        private ILogger<LayoutController> logger;

        public LayoutController(IStorageManager sm, ILogger<LayoutController> lg)
        {
            logger = lg;
            storageManager = sm;            
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
			return View();
		}

        [Navigation(IgnoreMove = true)]
        public IActionResult Settings()
		{
			return View();
		}
    }
}
