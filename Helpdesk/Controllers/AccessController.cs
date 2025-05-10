using Database;
using Domain.User;
using Microsoft.AspNetCore.Identity;

namespace Helpdesk.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private HelpdeskDbContext _context;

        public AccessController(UserManager<User> userManager, SignInManager<User> signInManager, HelpdeskDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Navigation(NavigationName = "Přihlášení")]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [Navigation(BackwardMove = true)]
        public async Task<IActionResult> SignIn(string Email, string PasswordHash)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, PasswordHash))
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return View("LoginSuccess");
            }
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
