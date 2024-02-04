using Avalonia.Controls;
using AvaloniaLearning.Views;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaLearning.ViewModels;

public class NavigationViewModel : ViewModelBase
{
    [Reactive]
    public UserControl ContentView { get; private set; } = new AuthorizationView();

    public void ChangeToRegistration()
    {
        ContentView = new RegistrationView();
    }

    public void ChangeToAuthorization()
    {
        ContentView = new AuthorizationView();
    }
}

