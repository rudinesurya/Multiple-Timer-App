using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;
using System.Collections.ObjectModel;

namespace Multiple_Timer_App.ViewModels;

public partial class EditAndRestartDurationViewModel : ObservableObject, IQueryAttributable
{
    private readonly ITimerService timerService;

    public ObservableCollection<int> HourOptions { get; } = new();
    public ObservableCollection<int> MinuteOptions { get; } = new();
    public ObservableCollection<int> SecondOptions { get; } = new();

    [ObservableProperty]
    CountdownTimer timer;

    [ObservableProperty]
    private int selectedHour;

    [ObservableProperty]
    private int selectedMinute;

    [ObservableProperty]
    private int selectedSecond;

    public EditAndRestartDurationViewModel(ITimerService timerService)
    {
        this.timerService = timerService;

        for (int i = 0; i < 24; i++) HourOptions.Add(i);
        for (int i = 0; i < 60; i += 1) MinuteOptions.Add(i);
        for (int i = 0; i < 60; i += 1) SecondOptions.Add(i);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("timerId", out var idObj) && idObj is string id)
        {
            Timer = timerService.Timers.FirstOrDefault(t => t.Id == id);

            if (Timer != null)
            {
                var duration = Timer.Duration;
                SelectedHour = duration.Hours;
                SelectedMinute = duration.Minutes;
                SelectedSecond = duration.Seconds;
            }
        }
    }

    [RelayCommand]
    private async void SaveDuration()
    {
        var newDuration = new TimeSpan(SelectedHour, SelectedMinute, SelectedSecond);

        // Serialize the newDuration to pass it as a query parameter
        var queryString = $"..?updatedDuration={newDuration.TotalSeconds}";

        // Go back one page and pass the updatedDuration
        await Shell.Current.GoToAsync(queryString);
    }
}
