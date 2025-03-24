using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class User : IdentityUser
{
    public UserType UserType { get; set; }
}
