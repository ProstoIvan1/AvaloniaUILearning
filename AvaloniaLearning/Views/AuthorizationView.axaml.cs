using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaLearning.ViewModels;

namespace AvaloniaLearning.Views;

public partial class AuthorizationView : UserControl
{
    public AuthorizationView()
    {
        InitializeComponent();
        DataContext = new AuthorizationViewModel();
    }
}
