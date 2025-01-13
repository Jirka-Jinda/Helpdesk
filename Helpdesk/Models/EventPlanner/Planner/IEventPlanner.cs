namespace Helpdesk.Models.EventPlanner.Planner
{
    /// <summary>
    /// Inherits IHostedService and IDisposable
    /// </summary>
    public interface IEventPlanner : IHostedService, IDisposable
    {
        void AddTask(Action task, EventPriority priority);
        void AddTimer(EventTimer timer);
    }
}
