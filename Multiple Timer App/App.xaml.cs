using Microsoft.Maui;
using Microsoft.Maui.Controls;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
#endif

namespace Multiple_Timer_App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());

#if WINDOWS
        window.Created += (s, e) =>
        {
            var mauiWindow = (Microsoft.Maui.Controls.Window)s;

            if (mauiWindow?.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
            {
                IntPtr hwnd = WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
                AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

                if (appWindow is not null)
                {
                    appWindow.Resize(new SizeInt32(1440/4, 3088/4)); // s23u ratio size
                }
            }
        };
#endif

        return window;
    }
}
