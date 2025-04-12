using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;

namespace Multiple_Timer_App.ViewModels;

public partial class TimerDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly ITimerService _timerService;

    [ObservableProperty]
    CountdownTimer timer;

    [ObservableProperty]
    private string pauseButtonText = "Pause";

    public TimerDetailViewModel(ITimerService timerService)
    {
        _timerService = timerService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("timerId", out var idObj) && idObj is string id)
        {
            Timer = _timerService.Timers.FirstOrDefault(t => t.Id == id);
        }
    }

    [RelayCommand]
    void TogglePause()
    {
        if (Timer != null)
        {
            if (Timer.IsRunning)
            {
                _timerService.StopTimer(Timer);
                PauseButtonText = "Resume";
            } else
            {
                _timerService.StartTimer(Timer);
                PauseButtonText = "Pause";
            }
        }
    }

    [RelayCommand]
    void RestartTimer()
    {
        if (Timer != null)
        {
            _timerService.RestartTimer(Timer);
            PauseButtonText = "Pause";
        }
    }
}
