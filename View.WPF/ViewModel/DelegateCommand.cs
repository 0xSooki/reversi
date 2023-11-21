using System;
using System.Windows.Input;

namespace ViewWPF
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object?> _execute;
        private readonly Func<Object?, Boolean>? _canExecute;

        public DelegateCommand(Action<Object?> execute) : this(null, execute) { }

        /// <summary>
        /// Create command
        /// </summary>
        /// <param name="canExecute"></param>
        /// <param name="execute"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DelegateCommand(Func<Object?, Boolean>? canExecute, Action<Object?> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Executeable check
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Boolean CanExecute(Object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Execute(Object? parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }

}
