using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PipelineBuddy.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            await ExecuteAync(parameter);
        }

        protected abstract Task ExecuteAync(object? parameter);
    }
}
