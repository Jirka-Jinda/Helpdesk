using Microsoft.AspNetCore.Identity;

namespace Domain.User;

public class UserRole : IdentityRole
{
    public UserType UserType { get; set; }

    public UserRole()
    {
        
    }

    public UserRole(UserType type) : base()
    {
        UserType = type;
    }
}
