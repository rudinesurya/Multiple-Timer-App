using Multiple_Timer_App.Models;
using Multiple_Timer_App.ViewModels;

namespace Multiple_Timer_App.Views;

public partial class TimerDetailPage : ContentPage
{
	public TimerDetailPage(TimerDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}