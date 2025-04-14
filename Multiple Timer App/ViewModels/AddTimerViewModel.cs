using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;
using System.Collections.ObjectModel;

namespace Multiple_Timer_App.ViewModels;

public partial class AddTimerViewModel : ObservableObject
{
    private readonly ITimerService timerService;

    public ObservableCollection<int> HourOptions { get; } = new();
    public ObservableCollection<int> MinuteOptions { get; } = new();
    public ObservableCollection<int> SecondOptions { get; } = new();

    [ObservableProperty]
    private int selectedHour;

    [ObservableProperty]
    private int selectedMinute;

    [ObservableProperty]
    private int selectedSecond;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string reminderNote;

    public AddTimerViewModel(ITimerService timerService)
    {
        this.timerService = timerService;

        for (int i = 0; i < 24; i++) HourOptions.Add(i);
        for (int i = 0; i < 60; i += 1) MinuteOptions.Add(i);
        for (int i = 0; i < 60; i += 1) SecondOptions.Add(i);

        SelectedHour = 0;
        SelectedMinute = 1; // 👈 default value
        SelectedSecond = 0;
    }

    [RelayCommand]
    private async void OnAddTimer()
    {
        TimeSpan selectedDuration = new TimeSpan(SelectedHour, SelectedMinute, SelectedSecond);

        var timer = new CountdownTimer
        {
            Title = Title,
            Duration = selectedDuration,
            RemainingTime = selectedDuration,
            ReminderNote = ReminderNote,
            IsRunning = false
        };

        timerService.AddTimer(timer);
        timerService.StartTimer(timer);

        await Shell.Current.GoToAsync(".."); // ✅ Go back one page
    }
}
