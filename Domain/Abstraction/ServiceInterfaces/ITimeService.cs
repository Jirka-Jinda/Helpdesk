namespace Domain.Abstraction.ServiceInterfaces;

public interface ITimeService
{
    public DateTime Now();
    public DateTime UtcNow();
    public void SetProviderOffset(TimeSpan offset);
    public void ResetProviderOffset();
}
