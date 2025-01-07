namespace Helpdesk.Models.EventPlanner.Manager
{
    /// <summary>
    /// Interface for communication with background service.
    /// Background service is responsible for scheduling and executing application tasks and events like restarts, updates and others.
    /// </summary>
    public interface IEventPlannerManager
    {
        void ScheduleDelayedTask(Action task, TimeSpan delay);
        void ScheduleTask(Action action, EventPriority priority);
        void ScheduleTask(EventCategory category);
        void ScheduleTimer(EventTimer timer);
        void ScheduleTimer(TimeSpan interval, Action action, CancellationTokenSource stoppingToken);
    }
}
