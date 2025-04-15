using Multiple_Timer_App.ViewModels;

namespace Multiple_Timer_App.Views;

public partial class EditAndRestartDurationPage : ContentPage
{
    public EditAndRestartDurationPage(EditAndRestartDurationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}