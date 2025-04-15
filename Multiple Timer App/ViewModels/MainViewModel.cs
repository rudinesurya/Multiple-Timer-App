using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;
using Multiple_Timer_App.Views;
using System.Collections.ObjectModel;

namespace Multiple_Timer_App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ITimerService timerService;

    // Collection of countdown timers
    public ObservableCollection<CountdownTimer> Timers => timerService.Timers;

    public MainViewModel(ITimerService timerService)
    {
        this.timerService = timerService;
    }

    // Navigate to AddTimerPage
    [RelayCommand]
    async Task AddTimer()
    {
        // Navigate to AddTimerPage
        await AppShell.Current.GoToAsync(nameof(AddTimerPage));
    }

    [RelayCommand]
    void DeleteTimer(CountdownTimer timer)
    {
        timerService.RemoveTimer(timer);
    }

    [RelayCommand]
    async Task NavigateToDetail(CountdownTimer timer)
    {
        if (timer != null)
        {
            await Shell.Current.GoToAsync(nameof(TimerDetailPage), new Dictionary<string, object>
            {
                { "timerId", timer.Id }
            });
        }
    }

    [RelayCommand]
    void TogglePause(CountdownTimer timer)
    {
        if (timer != null)
        {
            if (timer.IsRunning)
            {
                timerService.StopTimer(timer);
            }
            else
            {
                timerService.StartTimer(timer);
            }
        }
    }

    [RelayCommand]
    void RestartTimer(CountdownTimer timer)
    {
        if (timer != null)
        {
            timerService.RestartTimer(timer);
        }
    }
}
