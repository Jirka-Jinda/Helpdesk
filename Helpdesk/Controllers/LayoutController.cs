using Helpdesk.Models.Attributes;

namespace Helpdesk.Controllers
{
	public class LayoutController : Controller
	{
        public IActionResult Index()
		{
            return View("Test");
		}

		[Navigation(IgnoreActionInNavigation = true)]
		public IActionResult Search(string search)
		{
            Console.WriteLine(search);

			return View("Index");
		}

        [Navigation(IgnoreActionInNavigation = true)]
        public IActionResult SwitchTheme()
        {
			//if (StaticMethods.GetFromContextItems(HttpContext.Items, out UserSettings userSettings))
			//{
			//	userSettings.SwitchTheme();
			//	StaticMethods.SetToContextItems(HttpContext.Items, userSettings);
			//}

            return View("Index");
        }

		public IActionResult Users()
		{
			return View();
		}

        public IActionResult Settings()
		{
			return View();
		}
    }
}
