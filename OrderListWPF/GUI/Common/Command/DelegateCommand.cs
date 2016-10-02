using System;
using System.Diagnostics;
using System.Windows.Input;

namespace OrderListWPF.GUI.Common.Command
{
    /// <summary>
    /// Base command class
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Create a command that can be run always
        /// </summary>
        /// <param name="execute">command action</param>
        public DelegateCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
        }

        /// <summary>
        /// Create a command that can be run always with group conversion
        /// </summary>
        /// <param name="execute">command action</param>
        public DelegateCommand(Action execute) : this(x => execute())
        {
        }

        /// <summary>
        /// Create command that can be run only if predicate equal to true
        /// </summary>
        /// <param name="execute">command action</param>
        /// <param name="canExecute">sction predicate</param>
        public DelegateCommand(Action execute, Func<bool> canExecute) : this(execute)
        {
            _canExecute = x => canExecute();
        }

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
