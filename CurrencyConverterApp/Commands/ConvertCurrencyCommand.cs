using CurrencyConverterApp.ViewModels;
using System;
using System.Windows.Input;

namespace CurrencyConverterApp.Commands
{
    public class ConvertCurrencyCommand : ICommand
    {
        private readonly CurrencyViewModel _currencyViewModel;

        public ConvertCurrencyCommand(CurrencyViewModel currencyViewModel)
        {
            _currencyViewModel = currencyViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
           await _currencyViewModel.ConvertCurrency();
        }
    }
}
