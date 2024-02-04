using Avalonia.Media;
using AvaloniaLearning.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace AvaloniaLearning.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        [Reactive]
        public string? Name { get; set; }
        [Reactive]
        public string? ClassGrade { get; set; }
        [Reactive]
        public string? UserName { get; set; }
        [Reactive]
        public string? Password { get; set; }

        private SuccessStatus SuccessStatus { get; set; } = new SuccessStatus("Account created", "This username is already exist");

        [ObservableAsProperty]
        public string? SuccessText => SuccessStatus.Text;

        [ObservableAsProperty]
        public IBrush? SuccessBrush => SuccessStatus.TextBrush;

        public ReactiveCommand<Unit, Unit> CreateAccount { get; set; }

        public RegistrationViewModel()
        {
            IObservable<bool> canCreate = this.WhenAnyValue(x => x.Name, x => x.ClassGrade, x => x.UserName, x => x.Password, CanCreate);
            CreateAccount = ReactiveCommand.CreateFromTask(CreateAccountMethod, canCreate);

            IObservable<string?> successTextObservable = SuccessStatus.WhenAnyValue(x => x.Text);
            successTextObservable.ToPropertyEx(this, x => x.SuccessText);

            IObservable<IBrush?> successBrushObservable = SuccessStatus.WhenAnyValue(x => x.TextBrush);
            successBrushObservable.ToPropertyEx(this, x => x.SuccessBrush);
        }

        private async Task CreateAccountMethod()
        {
            using (AppData db = new AppData())
            {
                if(Name != null && ClassGrade != null && UserName != null && Password != null)
                {
                    bool isExist = await db.Users.Where(user => user.Username == UserName).AnyAsync();
                    if (isExist)
                    {
                        SuccessStatus.SetUnsuccess();
                        return;
                    }

                    EfUser user = new EfUser(UserName, Password, Name, ClassGrade);
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    SuccessStatus.SetSuccess();
                }
            }
        }

        private static bool CanCreate(string? name, string? classGrade, string? userName, string? password)
        {
            return
                !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(classGrade) &&
                !string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password);
        }

    }
}
