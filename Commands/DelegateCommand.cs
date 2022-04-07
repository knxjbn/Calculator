using System;
using System.Windows.Input;

namespace Calculator.Commands
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _executeMethod = null;
        private readonly Func<T, bool> _canExecuteMethod = null;

        public bool CanExecute(T parameter)
        {
            if (_canExecuteMethod != null)
                return _canExecuteMethod(parameter);
            return true;
        }

        public void Execute(T parameter)
        {
            _executeMethod?.Invoke(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null &&
                typeof(T).IsValueType)
            {
                return _canExecuteMethod == null;
            }
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}