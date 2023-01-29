using CurrencyConverterApp.Commands;
using System.Net.Http;
using System.Windows;
using System;
using System.Windows.Input;
using System.Threading.Tasks;

namespace CurrencyConverterApp.ViewModels
{
    public class CurrencyViewModel : ViewModelBase
    {
        private string _amountInNumbers = string.Empty;
        private string _amountInWords = string.Empty;
        private readonly ConvertCurrencyCommand _convertCurrencyCommand;
        private static readonly string url = "https://localhost:7239/api/DollarConverter";

        public CurrencyViewModel()
        {
            _convertCurrencyCommand = new ConvertCurrencyCommand(this);
        }

        public ICommand ConvertCurrencyCommand
        {
            get
            {
                return _convertCurrencyCommand;
            }
        }

        public async Task ConvertCurrency()
        {
            using HttpClient client = new();
            try
            {
                var apiUrl = $"{url}?amount={AmountInNumbers}";
                var response = await client.GetAsync($"{apiUrl}");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    AmountInWords = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"something went wrong {ex.Message}");
            }
        }

        #region public properties
        public string AmountInNumbers
        {
            get { return _amountInNumbers!; }

            set
            {
                _amountInNumbers = value;
                OnPropertyChanged(nameof(AmountInNumbers));
            }
        }

        public string AmountInWords
        {
            get { return _amountInWords!; }

            set
            {
                _amountInWords = value;
                OnPropertyChanged(nameof(AmountInWords));
            }
        }

        #endregion
    }
}
