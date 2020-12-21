using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Kniffel.ViewModels.Base
{

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> action;

        public DelegateCommand(Action<T> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action action) : base(o => action())
        {
        }
    }
}

