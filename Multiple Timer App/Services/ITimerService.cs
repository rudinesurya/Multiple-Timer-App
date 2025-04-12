using Multiple_Timer_App.Models;
using System.Collections.ObjectModel;

namespace Multiple_Timer_App.Services;

public interface ITimerService
{
    ObservableCollection<CountdownTimer> Timers { get; }
    void AddTimer(CountdownTimer timer);
    void RemoveTimer(CountdownTimer timer);
    void StartTimer(CountdownTimer timer);
    void StopTimer(CountdownTimer timer);
    void RestartTimer(CountdownTimer timer);
}