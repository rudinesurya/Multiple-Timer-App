using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;

namespace Multiple_Timer_App.ViewModels;

public partial class TimerDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly ITimerService timerService;

    [ObservableProperty]
    CountdownTimer timer;

    [ObservableProperty]
    private string pauseResumeButtonText = "Pause";

    public TimerDetailViewModel(ITimerService timerService)
    {
        this.timerService = timerService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("timerId", out var idObj) && idObj is string id)
        {
            Timer = timerService.Timers.FirstOrDefault(t => t.Id == id);
        }
    }

    [RelayCommand]
    void TogglePause()
    {
        if (Timer != null)
        {
            if (Timer.IsRunning)
            {
                timerService.StopTimer(Timer);
                PauseResumeButtonText = "Resume";
            } else
            {
                timerService.StartTimer(Timer);
                PauseResumeButtonText = "Pause";
            }
        }
    }

    [RelayCommand]
    void RestartTimer()
    {
        if (Timer != null)
        {
            timerService.RestartTimer(Timer);
            PauseResumeButtonText = "Pause";
        }
    }
}
