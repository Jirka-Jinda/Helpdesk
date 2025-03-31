using Domain.Ticket;
using Domain.User;
using System.Text;

namespace Database;

public class DbDataBuilder
{
    private HelpdeskDbContext _context;
    private Random _random;
    private int _createdTicketCount = 150;
    private (int Solvers, int Users) _createdUserCount = (30, 70);
    private static bool _usersCreated = false;

    public DbDataBuilder(HelpdeskDbContext context)
    {
        _context = context;
        _random = new Random();
    }

    public DbDataBuilder PopulateTickets()
    {
        for (int count = 0; count < _createdTicketCount; count++)
        {
            Ticket newTicket = new Ticket();
            newTicket.Type = TicketType.Základní;
            newTicket.Header = GenerateRandomText(4, 12);
            newTicket.Description = GenerateRandomText(25, 500);

            if (_usersCreated)
            {
                var randomUser = _context.Users
                    .Skip(count % (_createdUserCount.Users + _createdUserCount.Solvers))
                    .FirstOrDefault();
                if (randomUser != null)
                    newTicket.UserCreated = newTicket.UserLastModified = randomUser;
            }

            _context.Tickets.Add(newTicket);
        }              
        _context.SaveChanges();

        return this;
    }

    public DbDataBuilder PopulateUsers()
    {
        List<User> newUsers = new List<User>();
        List<string> firstNames = ["Esme", "Finley", "Amir", "Zaniyah", "Aislinn", "Joelle", "Heidi", "Marcellus", "Clare", "Leon", "Brady",
            "Taylor", "Robin ", "Zola", "Alvaro", "Ashton", "Kallie", "Derek", "Chandler", "Marlowe", "Alex", "Gordon", "Richard", "Regina"];
        List<string> lastNames = ["Glass", "Krueger", "Black", "Sweeney", "Browning", "Fuller", "Casey", "Sims", "Phillips", "Barrera",
            "Valenzuela", "Hubbard", "York", "Conway", "Lambert", "Daniels", "Meadows", "Holloway", "Douglas", "Odom", "Li", "Hayes", "Jenkins"];

        for (int count = 0; count < _createdUserCount.Solvers + _createdUserCount.Users; count++)
        {
            User newUser = new User();
            string lastName = lastNames[_random.Next(0, lastNames.Count)];
            newUser.UserName = firstNames[_random.Next(0, firstNames.Count)] + " " + lastName;
            newUser.Email = $"{lastName}{count}@domain.com";
            newUser.PhoneNumber = _random.Next(100_000_000, 999_999_999).ToString();

            if (count < _createdUserCount.Solvers)
                newUser.UserType = UserType.Řešitel;
            else
                newUser.UserType = UserType.Zadavatel;

            _context.Users.Add(newUser);            
        }
        _context.SaveChangesAsync();
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
            if (randValue > 26)
                str.Append(" ");
            else
                str.Append(Convert.ToChar(randValue + 65));
        }
        return str.ToString();
    }
}
