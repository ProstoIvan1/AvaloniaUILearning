using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaLearning.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaLearning.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [ObservableAsProperty]
        public UserControl CurrentView { get; private set; }

        public MainWindowViewModel()
        {
            IObservable<UserControl> viewObservable = this.WhenAnyValue(x => x.CurrentView);
            viewObservable.ToPropertyEx(this, x => x.CurrentView);

            CurrentView = new AuthorizationView();
        }

        public void OpenRegistrationView()
        {
            CurrentView = new RegistrationView();
        }
    }
}
