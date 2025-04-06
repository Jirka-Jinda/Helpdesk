using Domain.User;
using Microsoft.AspNetCore.Identity;

public class UserService(UserManager<User> userManager, SignInManager<User> singInManager)
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = singInManager;

    public UserManager<User> GetUserManager()
    {
        return _userManager;
    }

    public SignInManager<User> GetSignInManager()
    {
        return _signInManager;
    }
}
