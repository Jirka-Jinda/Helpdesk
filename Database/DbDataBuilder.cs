using Database.Repositories.Abstraction;
using Domain.Ticket;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Database;

public class DbDataBuilder
{
    private Random _random;
    private ITicketRepository _ticketRepository;
    private HelpdeskDbContext _context;
    private UserManager<User> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    private int _createdTicketCount = 150;
    private (int Solvers, int Users) _createdUserCount = (30, 70);
    private static bool _usersCreated = false;

    public DbDataBuilder(IServiceProvider serviceProvider)
    {
        _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        _context = serviceProvider.GetRequiredService<HelpdeskDbContext>();
        _ticketRepository = serviceProvider.GetRequiredService<ITicketRepository>();
        _random = new Random();
    }

    public async Task<DbDataBuilder> GenerateTicketsAsync()
    {
        if (_context.Tickets.Any())
            return this;

        for (int count = 0; count < _createdTicketCount; count++)
        {
            Ticket newTicket = new Ticket();
            newTicket.Type = TicketType.Základní;
            newTicket.Header = GenerateRandomText(10, 25);
            newTicket.Description = GenerateRandomText(25, 500);

            if (_usersCreated)
            {
                var randomUser = _context.Users
                    .Skip(count % (_createdUserCount.Users + _createdUserCount.Solvers))
                    .FirstOrDefault();
                if (randomUser != null)
                    newTicket.UserCreated = newTicket.UserLastModified = randomUser;
            }
            await _ticketRepository.CreateAsync(newTicket);
        }              

        return this;
    }

    public async Task<DbDataBuilder> GenerateUsersAsync()
    {
        if (_context.Users.Any())
            return this;

        List<User> newUsers = new List<User>();
        List<string> firstNames = ["Esme", "Finley", "Amir", "Zaniyah", "Aislinn", "Joelle", "Heidi", "Marcellus", "Clare", "Leon", "Brady",
            "Taylor", "Robin", "Zola", "Alvaro", "Ashton", "Kallie", "Derek", "Chandler", "Marlowe", "Alex", "Gordon", "Richard", "Regina"];
        List<string> lastNames = ["Glass", "Krueger", "Black", "Sweeney", "Browning", "Fuller", "Casey", "Sims", "Phillips", "Barrera",
            "Valenzuela", "Hubbard", "York", "Conway", "Lambert", "Daniels", "Meadows", "Holloway", "Douglas", "Odom", "Li", "Hayes", "Jenkins"];

        string password = "Password123!";

        for (int count = 0; count < _createdUserCount.Solvers + _createdUserCount.Users; count++)
        {
            User newUser = new User();
            string lastName = lastNames[_random.Next(0, lastNames.Count)];
            newUser.UserName = firstNames[_random.Next(0, firstNames.Count)] + "_" + lastName;
            newUser.Email = $"{lastName}{count}@domain.com";
            newUser.PhoneNumber = _random.Next(000_000_000, 999_999_999).ToString();
            UserType role = UserType.Řešitel;

            if (count > _createdUserCount.Solvers)
                role = UserType.Zadavatel;

            newUser.UserRole = role;

            var checkDuplicity = await _userManager.FindByNameAsync(newUser.UserName);
            if (checkDuplicity is null)
            {
                var userRes = await _userManager.CreateAsync(newUser, password);
                var roleRes = await _userManager.AddToRoleAsync(newUser, newUser.UserRole.ToString());

                if (!userRes.Succeeded || !roleRes.Succeeded)
                    throw new Exception("Seeding db with users failed.");
            }
        }

        _usersCreated = true;

        return this;
    }

    private string GenerateRandomText(int minLen, int maxLen)
    {
        int stringlen = _random.Next(minLen, minLen);
        int randValue;
        StringBuilder str = new();
        for (int i = 0; i < stringlen; i++)
        {
            randValue = _random.Next(0, 29);
            if (randValue > 18)
                str.Append(" ");
            else
                str.Append(Convert.ToChar(randValue + 65));
        }
        return str.ToString();
    }

    public async Task<DbDataBuilder> GenerateRolesAsync()
    {
        if (_context.Roles.Any())
            return this;

        foreach (var roleName in Enum.GetNames(typeof(UserType)))
        {
            var roleRes = await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            if (!roleRes.Succeeded)
                throw new Exception("Seeding db with roles failed.");
        }

        return this;
    }

    public async Task<DbDataBuilder> GenerateAdminAsync()
    {
        if (_context.Users.Any(u => u.UserName == "admin"))
            return this;

        Console.WriteLine("Creating admin user...");

        var user = new User()
        {
            UserName = "admin",
            UserRole = UserType.Řešitel,
            Email = "admin@domain.com"
        };
        string password = "Password123!";

        var userCreated = await _userManager.CreateAsync(user, password);
        var roleCreated = await _userManager.AddToRoleAsync(user, user.UserRole.ToString());

        user = await _context.Users.Where(u => u.UserName == "admin").FirstOrDefaultAsync();

        if (!userCreated.Succeeded || !roleCreated.Succeeded || user == default)
            throw new Exception("Seeding db with admin user failed.");

        return this;
    }
}
