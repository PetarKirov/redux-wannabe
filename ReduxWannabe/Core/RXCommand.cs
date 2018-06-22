using System;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ReduxWannabe.Core
{
    public abstract class RXCommand : ICommand
    {
        private bool _canExecute;

        protected RXCommand(IObservable<bool> canExecute = null)
        {
            if (canExecute == null)
                canExecute = Observable.Return(true);

            canExecute.DistinctUntilChanged()
                .ObserveOnDispatcher()
                .Subscribe(x =>
                {
                    _canExecute = x;
                    CanExecuteChanged?.Invoke(this, new EventArgs());
                });
        }

        public event EventHandler CanExecuteChanged;
        public abstract void Execute(object parameter);

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }
    }

    public sealed class RXBasicCommand : RXCommand
    {
        private readonly Action _action;

        public RXBasicCommand(Action action, IObservable<bool> canExecute = null)
            : base(canExecute)
        {
            _action = action ?? throw new ArgumentNullException();
        }

        public override void Execute(object _)
        {
            _action?.Invoke();
        }
    }

    public sealed class RXParamCommand<T> : RXCommand
    {
        private readonly Action<T> _action;

        public RXParamCommand(Action<T> action, IObservable<bool> canExecute = null)
            : base(canExecute)
        {
            _action = action ?? throw new ArgumentNullException();
        }

        public override void Execute(object arg)
        {
            _action?.Invoke((T)arg);
        }
    }
}
