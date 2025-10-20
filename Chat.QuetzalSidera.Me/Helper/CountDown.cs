namespace Chat.QuetzalSidera.Me.Helper;

using System.Timers;

public class CountDownTimer : IDisposable
{
    private readonly Timer _timer;
    private readonly Func<Task> _onEnd;
    private readonly Func<Task> _onTick;
    public int RemainingSeconds { get; private set; }


    public CountDownTimer(int startSeconds, Func<Task> onTick, Func<Task> onEnd)
    {
        _timer = new Timer(1000);
        RemainingSeconds = startSeconds;
        _timer.Elapsed += OnTick;
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _onEnd = onEnd;
        _onTick = onTick;
    }

    private void OnTick(object? source, ElapsedEventArgs e)
    {
        if (RemainingSeconds != 0)
        {
            RemainingSeconds--;
            _onTick.Invoke();
        }
        else
        {
            _onTick.Invoke();
            _onEnd.Invoke();
        }
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}