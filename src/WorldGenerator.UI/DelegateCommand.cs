using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorldGenerator.UI
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Func<T, bool>? _canExecuteAction;

        public DelegateCommand(Action<T> executeAction, Func<T, bool>? canExecuteAction = null)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) 
        {
            if (parameter is T param)
            {
                return _canExecuteAction?.Invoke(param) ?? true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T param)
            {
                _executeAction(param);
            }
            else
            {
                throw new ArgumentException($"Expected command parameter of incorrect type {parameter?.GetType()}");
            }
        }

        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool>? _canExecuteAction;

        public DelegateCommand(Action executeAction, Func<bool>? canExecuteAction = null)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecuteAction?.Invoke() ?? true;

        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public void Execute(object? parameter) => _executeAction();
    }
}
