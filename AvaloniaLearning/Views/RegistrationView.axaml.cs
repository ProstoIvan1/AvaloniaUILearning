using Avalonia.Controls;
using AvaloniaLearning.ViewModels;

namespace AvaloniaLearning.Views
{
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }
    }
}
