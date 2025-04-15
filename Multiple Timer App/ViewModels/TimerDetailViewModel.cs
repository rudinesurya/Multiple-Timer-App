using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Multiple_Timer_App.Models;
using Multiple_Timer_App.Services;
using Multiple_Timer_App.Views;

namespace Multiple_Timer_App.ViewModels;

public partial class TimerDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly ITimerService timerService;

    [ObservableProperty]
    CountdownTimer timer;

    [ObservableProperty]
    string editedTitle;

    [ObservableProperty]
    string editedReminderNote;

    // Temporary duration to be applied when the user clicks save
    [ObservableProperty]
    private TimeSpan? editedDuration;

    public TimerDetailViewModel(ITimerService timerService)
    {
        this.timerService = timerService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("timerId", out var idObj) && idObj is string id)
        {
            Timer = timerService.Timers.FirstOrDefault(t => t.Id == id);

            if (Timer != null)
            {
                EditedTitle = Timer.Title;
                EditedReminderNote = Timer.ReminderNote;
                EditedDuration = Timer.Duration;  // Keep track of the original duration
            }
        }

        // Check if duration was passed back from EditAndRestartDurationPage
        if (query.TryGetValue("updatedDuration", out var updatedDurationObj) && updatedDurationObj is string updatedDurationStr)
        {
            if (double.TryParse(updatedDurationStr, out var totalSeconds))
            {
                EditedDuration = TimeSpan.FromSeconds(totalSeconds);
            }
        }
    }

    [RelayCommand]
    private void SaveChanges()
    {
        if (Timer != null)
        {
            Timer.Title = EditedTitle?.Trim();
            Timer.ReminderNote = EditedReminderNote?.Trim();

            // Apply the edited duration if available
            if (EditedDuration.HasValue)
            {
                Timer.Duration = EditedDuration.Value;
                Timer.RemainingTime = EditedDuration.Value;
                timerService.RestartTimer(Timer);
            }
        }
    }

    [RelayCommand]
    private async void EditDuration()
    {
        if (Timer != null)
        {
            await Shell.Current.GoToAsync(nameof(EditAndRestartDurationPage), new Dictionary<string, object>
            {
                { "timerId", Timer.Id }
            });
        }
    }
}
