namespace Helpdesk.Models
{
    public abstract class BaseObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
