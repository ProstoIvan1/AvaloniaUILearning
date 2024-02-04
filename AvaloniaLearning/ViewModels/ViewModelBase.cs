using AvaloniaLearning.Models;
using ReactiveUI;

namespace AvaloniaLearning.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public User[] Users => UserData.Users;
}
