﻿using System;
using System.Windows.Input;

namespace ProcessTree.Utilities
{
    public abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute()
        {
            return true;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        public abstract void Execute();

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}