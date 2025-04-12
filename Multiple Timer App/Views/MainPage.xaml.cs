using Multiple_Timer_App.Models;
using Multiple_Timer_App.ViewModels;

namespace Multiple_Timer_App.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
