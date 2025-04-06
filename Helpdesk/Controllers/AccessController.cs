using Database;
using Domain.User;
using Microsoft.AspNetCore.Identity;

namespace Helpdesk.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserManager<User> _userManager;
        private HelpdeskDbContext _context;

        public AccessController(UserManager<User> userManager, HelpdeskDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Navigation(NavigationName = "Přihlášení")]
        public IActionResult Login()
        {
            return View();
        }

        //public IActionResult SignIn(User user)
        //{

        //}
    }
}
