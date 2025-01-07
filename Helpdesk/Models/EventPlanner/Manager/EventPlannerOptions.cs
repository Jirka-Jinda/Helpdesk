namespace Helpdesk.Models.EventPlanner.Manager
{
    public class EventPlannerOptions
    {
        /// <summary>
        /// Default timer for restarting application.
        /// </summary>
        public TimeOnly RestartTime { get; set; }
        public TimeSpan CleanupTimerInterval { get; set; }
    }
}
