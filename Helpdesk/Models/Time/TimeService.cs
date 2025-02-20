using Domain.Abstraction.ServiceInterfaces;
using System.Diagnostics;

namespace Helpdesk.Models.Time;

public class TimeService : ITimeService
{
    private readonly Stopwatch _applicationTime;
    private TimeSpan _offset { get; set; }

    public TimeService(TimeOptions options)
    {
        _applicationTime = new Stopwatch();
        _applicationTime.Start();
        _offset = TimeSpan.Zero;
    }

    public DateTime UtcNow()
    {
        return DateTime.UtcNow + _offset;
    }

    public DateTime Now()
    {
        return DateTime.Now + _offset;
    }

    public void ResetProviderOffset()
    {
        _offset = TimeSpan.Zero;
    }

    public void SetProviderOffset(TimeSpan offset)
    {
        _offset = offset;
    }
}
