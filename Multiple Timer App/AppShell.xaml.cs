using Multiple_Timer_App.Views;

namespace Multiple_Timer_App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AddTimerPage), typeof(AddTimerPage));
        Routing.RegisterRoute(nameof(TimerDetailPage), typeof(TimerDetailPage));
    }
}
