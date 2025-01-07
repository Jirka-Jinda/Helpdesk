namespace Helpdesk.Models.EventPlanner.Manager
{
    /// <summary>
    /// Implementation of IEventPlannerManager.
    /// </summary>
    public class EventPlannerManager : IEventPlannerManager
    {
        private readonly EventPlanner eventPlanner;

        public EventPlannerManager(EventPlanner ep)
        {
            eventPlanner = ep;
        }

        public void ScheduleTask(Action action, EventPriority priority)
        {
            eventPlanner.AddTask(action, priority);
        }
        
        public void ScheduleTask(EventCategory category)
        {
            //Action action;
            //EventPriority priority;

            switch (category)
            {
                case EventCategory.ApplicationShutdown:
                    break;
                case EventCategory.ApplicationRestart:
                    break;
                case EventCategory.DatabaseRestart:
                    break;
                default:
                    break;
            }
            //TODO ScheduleTask(action, priority);
        }

        public void ScheduleDelayedTask(Action task, TimeSpan delay) => eventPlanner.AddTimer(new EventTimer(delay, task, true));

        public void ScheduleTimer(EventTimer timer) => eventPlanner.AddTimer(timer);

        public void ScheduleTimer(TimeSpan interval, Action action, CancellationTokenSource stoppingToken) => eventPlanner.AddTimer(new EventTimer(interval, action, stoppingTokenSource: stoppingToken));
    }
}
