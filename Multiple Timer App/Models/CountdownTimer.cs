using CommunityToolkit.Mvvm.ComponentModel;

namespace Multiple_Timer_App.Models;

public partial class CountdownTimer : ObservableObject
{
    // Unique identifier for the timer
    public string Id { get; } = Guid.NewGuid().ToString();

    public Timer? InternalTimer { get; set; }

    [ObservableProperty]
    string title;

    [ObservableProperty]
    TimeSpan duration;

    [ObservableProperty]
    TimeSpan remainingTime;

    [ObservableProperty]
    bool isRunning;

    [ObservableProperty]
    string reminderNote;
}