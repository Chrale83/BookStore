﻿using System.Windows.Input;

namespace BookStore.Presentation.Command
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _exectue;
        private readonly Func<object?, bool>? _canExectue;

        public event EventHandler? CanExecuteChanged; //Fungerar som propertyChanged

        public RelayCommand(Action<object> exectue, Func<object?, bool>? canExectue = null) //Den kan vara en referns till en metod som tar ett object in
        {
            ArgumentNullException.ThrowIfNull(exectue);
            _exectue = exectue;
            _canExectue = canExectue;
        }

        public void RaiseCanExectueChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) //Den kör denna först(om jag använder den), sen Execute() Returnerar en bool
        {
            return _canExectue is null ? true : _canExectue(parameter); 
        }

        public void Execute(object? parameter) //Den koden som körs när man trycker på knappen, men bara om CanExecute() är true 
        {
            _exectue(parameter);
        }
    }
}
