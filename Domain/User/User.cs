using Microsoft.AspNetCore.Identity;

namespace Domain.User;

public class User : IdentityUser<Guid>
{
    public UserType UserType { get; set; }
}
