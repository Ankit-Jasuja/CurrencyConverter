using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace CurrencyConverterApp.ValidationRules
{
    public class CurrencyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, "Amount cannot be empty.");
            var input = string.Concat(value.ToString()!.Where(c => !char.IsWhiteSpace(c)));
            if (input.Contains(','))
            {
                if (IsDollarPartValid(input))
                {
                    return new ValidationResult(false, "numbers of dollars is greater than 999 999 999");
                }
                if (IsCentsPartValid(input))
                {
                    return new ValidationResult(false, "numbers of cents is greater than 99");
                }
            }
            double output;
            if (!double.TryParse(input, NumberStyles.Any,
                new NumberFormatInfo() { NumberDecimalSeparator = "," }, out output))
            {
                return new ValidationResult(false, "amount must be a number.");
            }
            if(output > 999999999 && !input.Contains(','))
            {
                return new ValidationResult(false, "numbers of dollars is greater than 999 999 999");
            }
            return ValidationResult.ValidResult;
        }

        private static bool IsCentsPartValid(string input)
        {
            return input.Split(',')[1].Length > 2;
        }

        private static bool IsDollarPartValid(string input)
        {
            return input.Split(',')[0].Length > 9;
        }
    }
}
