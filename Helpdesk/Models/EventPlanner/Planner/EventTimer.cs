namespace Helpdesk.Models.EventPlanner.Planner
{
    public class EventTimer
    {
        private readonly PeriodicTimer _timer;
        private readonly Action _action;
        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _stoppingToken;
        public bool IsCompleted => _timer.Period == Timeout.InfiniteTimeSpan;

        public EventTimer(TimeSpan period, Action action, bool executeOnce = false, TimeSpan? delayFirstExecution = null, CancellationTokenSource? stoppingTokenSource = null)
        {
            _timer = new PeriodicTimer(period);
            _action = action;

            if (stoppingTokenSource is null)
                _tokenSource = new CancellationTokenSource();
            else
                _tokenSource = stoppingTokenSource;
            _stoppingToken = _tokenSource.Token;

            // TODO delay

            if (executeOnce)
                Task.Run(TimerExecute);
            else
                Task.Run(TimerLoop);
        }

        private async void TimerLoop()
        {
            while (await _timer.WaitForNextTickAsync(_stoppingToken))
                _action.Invoke();
            _timer.Period = Timeout.InfiniteTimeSpan;
        }

        private async void TimerExecute()
        {
            await _timer.WaitForNextTickAsync(_stoppingToken);
            _action.Invoke();
            _timer.Period = Timeout.InfiniteTimeSpan;
        }

        public void CancelEvent() => _tokenSource.Cancel();

        public void ChangePeriod(TimeSpan newPeriod) => _timer.Period = newPeriod;

    }
}
