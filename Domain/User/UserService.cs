using Domain.User;
using Microsoft.AspNetCore.Http;

public class UserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserId()
    {        
        throw new NotImplementedException();
    }

    public User GetCurrentUser()
    {
        throw new NotImplementedException();
    }
}
