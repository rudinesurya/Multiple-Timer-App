using Multiple_Timer_App.ViewModels;

namespace Multiple_Timer_App.Views;

public partial class AddTimerPage : ContentPage
{
	public AddTimerPage(AddTimerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}