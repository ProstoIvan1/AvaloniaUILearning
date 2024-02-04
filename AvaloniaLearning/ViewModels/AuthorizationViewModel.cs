using Avalonia.Media;
using AvaloniaLearning.Models;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaLearning.ViewModels;

public class AuthorizationViewModel : ViewModelBase
{
    [Reactive]
    public string? Username { get; set; }

    [Reactive]
    public string? Password { get; set; }

    public ReactiveCommand<Unit, Unit> Login { get; }

    private SuccessStatus SuccessStatus { get; } = new SuccessStatus("You are in!", "Try again");

    [ObservableAsProperty]
    public string? SuccessText => SuccessStatus.Text;

    [ObservableAsProperty]
    public IBrush? SuccessBrush => SuccessStatus.TextBrush;

    public AuthorizationViewModel()
    {
        IObservable<bool> canLogIn = this.WhenAnyValue(x => x.Username, x => x.Password, CheckValues);
        Login = ReactiveCommand.CreateFromTask(TryToLogIn, canLogIn);

        IObservable<IBrush?> successBrushObservable = SuccessStatus.WhenAnyValue(x => x.TextBrush);
        successBrushObservable.ToPropertyEx(this, x => x.SuccessBrush);

        IObservable<string?> successTextObservable = SuccessStatus.WhenAnyValue(x => x.Text);
        successTextObservable.ToPropertyEx(this, x => x.SuccessText);
    }

    private bool CheckValues(string? username, string? password)
    {
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }

    private async Task TryToLogIn()
    {
        await Task.Delay(1000);
        if(await IsDbContainUser())
        {
            SuccessStatus.SetSuccess();
            return;
        }
        SuccessStatus.SetUnsuccess();
    }

    private async Task<bool> IsDbContainUser()
    {
        using(AppData db = new AppData())
        {
            EfUser? user = await db.Users.Where(x => x.Username.Equals(Username) && x.Password.Equals(Password)).FirstOrDefaultAsync();
            return user != null;
        }
    }
}
