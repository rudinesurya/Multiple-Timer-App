using Microsoft.Extensions.Logging;
using Multiple_Timer_App.Services;
using Multiple_Timer_App.ViewModels;
using Multiple_Timer_App.Views;

namespace Multiple_Timer_App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<ITimerService, TimerService>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<AddTimerViewModel>();
        builder.Services.AddTransient<AddTimerPage>();

        builder.Services.AddTransient<TimerDetailViewModel>();
        builder.Services.AddTransient<TimerDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
