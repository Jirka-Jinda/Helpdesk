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
        private PasswordHasher<User> _passwordHasher = new(); 

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
            PasswordVerificationResult checkPass = PasswordVerificationResult.Failed;

            if (user != null && user.PasswordHash != null)
                checkPass = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, PasswordHash);
            
            if (user != null && checkPass == PasswordVerificationResult.Success)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return View("LoginSuccess");
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
