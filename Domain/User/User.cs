using Microsoft.AspNetCore.Identity;

namespace Domain.User;

public class User : IdentityUser
{
    public UserType UserRole { get; set; }
}
