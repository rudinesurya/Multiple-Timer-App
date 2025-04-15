using CommunityToolkit.Mvvm.ComponentModel;
using Multiple_Timer_App.Models;
using System.Collections.ObjectModel;

namespace Multiple_Timer_App.Services;

public partial class TimerService : ObservableObject, ITimerService
{
    [ObservableProperty]
    private ObservableCollection<CountdownTimer> timers = new();

    public void AddTimer(CountdownTimer timer)
    {
        timers.Add(timer);
    }

    public void RemoveTimer(CountdownTimer timer)
    {
        if (timer != null)
        {
            StopTimer(timer);
            timers.Remove(timer);
        }
    }

    public void StartTimer(CountdownTimer timer)
    {
        if (timer.IsRunning || timer.InternalTimer != null)
            return;

        timer.IsRunning = true;

        timer.InternalTimer = new Timer(_ =>
        {
            if (!timer.IsRunning)
            {
                StopTimer(timer);
                return;
            }

            if (timer.RemainingTime.TotalSeconds > 0)
            {
                timer.RemainingTime = timer.RemainingTime.Subtract(TimeSpan.FromSeconds(1));

                if (timer.RemainingTime.TotalSeconds <= 0)
                {
                    timer.IsRunning = false;
                    StopTimer(timer);

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.DisplayAlert("Timer Finished", timer.ReminderNote ?? "Time's up!", "OK");
                    });
                }
            }
            else
            {
                StopTimer(timer);
            }

        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    public void StopTimer(CountdownTimer timer)
    {
        timer.IsRunning = false;

        timer.InternalTimer?.Dispose();
        timer.InternalTimer = null;
    }

    public void RestartTimer(CountdownTimer timer)
    {
        StopTimer(timer);
        timer.RemainingTime = timer.Duration; // or your default/original time
    }
}
