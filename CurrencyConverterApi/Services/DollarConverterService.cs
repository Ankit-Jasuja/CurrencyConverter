namespace CurrencyConverter.Services
{
    public class DollarConverterService : IConverterService
    {
        public string ConvertToWords(double value)
        {
            var money = Math.Round((decimal)value, 2);
            var number = (int)money;
            var doller = NumberToDollars(number);
            var cents = NumberToCents(money);

            var dollarTerm = doller == "one" ? "dollar" : "dollars";
            var centTerm = cents == "one" ? "cent" : "cents";

            var result = !string.IsNullOrEmpty(cents) ? $"{doller} {dollarTerm} and {cents} {centTerm}"
                : $"{doller} {dollarTerm}";
            return result;
        }

        private string NumberToDollars(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToDollars(Math.Abs(number));

            var words = string.Empty;

            if ((number / 1000000) > 0)
            {
                words += NumberToDollars(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToDollars(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToDollars(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                var units = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tens = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                {
                    words += units[number];
                }
                else
                {
                    words += tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + units[number % 10];
                }
            }
            return words.ToLower();
        }

        private string NumberToCents(decimal money)
        {
            var cents = string.Empty;
            if (money.ToString().Contains('.'))
            {
                int valueAfterDecimal;
                if (IsOneDigitAfterDecimal(money))
                {
                    var updatedMoney = money.ToString() + "0";
                    valueAfterDecimal = int.Parse(updatedMoney.Split('.')[1]);
                }
                else
                {
                    valueAfterDecimal = int.Parse(money.ToString().Split('.')[1]);
                }
                cents = NumberToDollars(valueAfterDecimal);
            }
            return cents;
        }

        private bool IsOneDigitAfterDecimal(decimal money)
        {
            return money.ToString().Split('.')[1].Length == 1;
        }
    }
}