namespace Helpdesk.Models.EventPlanner
{
    public partial class EventPlanner
    {
        public void AddTimer(EventTimer timer)
        {
            EventTimers.Add(timer);
        }

        public void AddTask(Action action, EventPriority priority)
        {
            TaskQueue.Enqueue(action, priority);

            if (!EventLoopRunning)
            {
                EventLoopRunning = true;
                Task.Run(EventLoop);
            }
        }

        private EventTimer CleanupTimer()
        {
            return new EventTimer(
                Options.CleanupTimerInterval,
                () =>
                {
                    int cleaned = 0;
                    for (int k = 0; k < EventTimers.Count; k++)
                    {
                        if (EventTimers[k].IsCompleted)
                        {
                            cleaned++;
                            EventTimers.RemoveAt(k);
                            k--;
                        }
                    }
                    Console.WriteLine($"Cleanup timer collecting garbage. Deleted: {cleaned} timers.");
                });
        }
    }
}
