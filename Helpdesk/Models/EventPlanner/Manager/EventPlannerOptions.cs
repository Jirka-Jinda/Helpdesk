namespace Helpdesk.Models.EventPlanner.Manager
{
    public class EventPlannerOptions
    {
        /// <summary>
        /// Default timer for restarting application.
        /// </summary>
        public TimeOnly RestartTime { get; set; } = new TimeOnly();
        public TimeSpan CleanupTimerInterval { get; set; } = new TimeSpan();
    }
}
