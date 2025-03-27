using System.ComponentModel.DataAnnotations;

namespace Domain.Abstraction;

public abstract class BaseDomainObject
{
    [Key]
    public Guid Id { get; set; }
    public DateTime TimeCreated { get; set; }
    public User.User UserCreated { get; set; }
    public DateTime TimeLastModified { get; set; }
    public User.User UserLastModified { get; set; }

    public BaseDomainObject()
    {
        //Id = Guid.NewGuid();
        TimeCreated = TimeLastModified = DateTime.UtcNow;
    }
}
