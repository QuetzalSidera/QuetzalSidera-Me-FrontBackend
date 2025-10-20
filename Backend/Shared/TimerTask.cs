using System.Timers;
using Timer = System.Timers.Timer;


namespace Backend.Shared;

/// <summary>
/// 定时任务类
/// </summary>
public class TimerTask
{
    private readonly Timer _timerInstance;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="callback">每延时时间会触发一次的回调函数</param>
    /// <param name="delay">毫秒为单位的延时时间</param>
    public TimerTask(ElapsedEventHandler callback, long delay)
    {
        _timerInstance = new Timer(delay);
        // 绑定事件处理程序
        _timerInstance.Elapsed += callback;
        _timerInstance.AutoReset = true; // 设置为重复执行
        _timerInstance.Enabled = true; // 启动定时器
    }


    public void StopTimer()
    {
        _timerInstance.Stop();
        _timerInstance.Dispose();
        Console.WriteLine("定时器已停止");
    }
}