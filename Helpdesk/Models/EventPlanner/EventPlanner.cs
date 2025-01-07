﻿namespace Helpdesk.Models.EventPlanner
{
    public partial class EventPlanner : BaseObject, IEventPlanner
    {
        private PriorityQueue<Action, EventPriority> TaskQueue { get; set; }
        private bool EventLoopRunning { get; set; }
        private List<EventTimer> EventTimers { get; set; }
        private ILogger<EventPlannerManager> Logger { get; set; }
        private EventPlannerOptions Options { get; set; }

        public EventPlanner(ILogger<EventPlannerManager> logger, IApplicationSettings settings)
        {
            Logger = logger;
            Options = settings.EventPlannerOptions;
            TaskQueue = new PriorityQueue<Action, EventPriority>();
            EventTimers = new List<EventTimer>();
            AddTimer(CleanupTimer());
        }

        private async void EventLoop()
        {
            while (TaskQueue.Count > 0)
            {
                var action = TaskQueue.Dequeue();
                await Task.Run(action);
            }
            EventLoopRunning = false;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
