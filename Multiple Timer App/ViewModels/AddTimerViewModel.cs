using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;

namespace Multiple_Timer_App.ViewModels;

public partial class AddTimerViewModel : ObservableObject
{
    private readonly ITimerService _timerService;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private TimeSpan selectedDuration = TimeSpan.FromMinutes(1);

    [ObservableProperty]
    private string reminderNote;

    public AddTimerViewModel(ITimerService timerService)
    {
        _timerService = timerService;
    }

    [RelayCommand]
    private async void OnAddTimer()
    {
        var timer = new CountdownTimer
        {
            Title = Title,
            Duration = SelectedDuration,
            RemainingTime = SelectedDuration,
            ReminderNote = ReminderNote,
            IsRunning = false
        };

        _timerService.AddTimer(timer);
        _timerService.StartTimer(timer);

        await AppShell.Current.GoToAsync("///MainPage");
    }
}
