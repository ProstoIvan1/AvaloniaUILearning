using Avalonia.Media;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaLearning.ViewModels
{
    internal class SuccessStatus : ReactiveObject
    {
        [Reactive]
        public string? Text { get; private set; }
        [Reactive]
        public IBrush? TextBrush { get; private set; }

        private string _successText;
        private string _unsuccessText;

        private IBrush _successBrush = Brushes.Green;
        private IBrush _unsuccessBrush = Brushes.DarkRed;

        public SuccessStatus(string successText, string unsuccessText)
        {
            _successText = successText;
            _unsuccessText = unsuccessText;

        }

        public SuccessStatus(string successText, string unsuccessText, IBrush successBrush, IBrush unsuccessBrush) : this(successText, unsuccessText)
        {
            _successBrush = successBrush;
            _unsuccessBrush = unsuccessBrush;
        }

        public event Action? OnSuccess;

        public virtual void SetSuccess()
        {
            Text = _successText;
            TextBrush = _successBrush;
            OnSuccess?.Invoke();
        }

        public event Action? OnUnsuccess;

        public virtual void SetUnsuccess()
        {
            Text = _unsuccessText;
            TextBrush = _unsuccessBrush;
            OnUnsuccess?.Invoke();
        }
    }
}
