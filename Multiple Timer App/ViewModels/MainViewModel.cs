using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;
using Multiple_Timer_App.Views;
using System.Collections.ObjectModel;

namespace Multiple_Timer_App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ITimerService _timerService;

    // Collection of countdown timers
    public ObservableCollection<CountdownTimer> Timers => _timerService.Timers;

    public MainViewModel(ITimerService timerService)
    {
        _timerService = timerService;
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
        _timerService.RemoveTimer(timer);
    }

    [RelayCommand]
    async Task NavigateToDetail(CountdownTimer timer)
    {
        if (timer != null)
        {
            await AppShell.Current.GoToAsync($"{nameof(TimerDetailPage)}?timerId={timer.Id}");
        }
    }
}
