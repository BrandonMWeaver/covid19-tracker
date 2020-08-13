using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace COVID19TrackerUI.Models.Base
{
    class CommandBase<T> : ICommand where T : class
    {
        private readonly Action<T> function;

        public event EventHandler CanExecuteChanged;

        public CommandBase(Action<T> function)
        {
            this.function = function;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.function.Invoke(parameter as T);
        }
    }
}
