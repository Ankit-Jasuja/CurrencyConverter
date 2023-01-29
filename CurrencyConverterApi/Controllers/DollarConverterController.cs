using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DollarConverterController : ControllerBase
    {
        private const int dollarsValue = 999999999;
        private readonly IConverterService _converterService;

        public DollarConverterController(IConverterService converterService)
        {
            _converterService = converterService;
        }

        [HttpGet]
        public ActionResult<string> Get(string amount)
        {
            var input = string.Concat(amount.Where(c => !char.IsWhiteSpace(c)));
            if (!double.TryParse(input, NumberStyles.Any, new NumberFormatInfo() { NumberDecimalSeparator = "," },
                out double value))
            {
                return BadRequest("input must be a number.");
            }
            if (amount.Contains(','))
            {
                if (IsDollarPartValid(input))
                {
                    return BadRequest("numbers of dollars is greater than 999 999 999");
                }
                if (IsCentsPartValid(input))
                {
                    return BadRequest("numbers of cents is greater than 99");
                }
            }
            if (value > dollarsValue && !input.Contains(','))
            {
                return BadRequest("numbers of dollars is greater than 999 999 999");
            }
            var result = _converterService.ConvertToWords(value);
            return Ok(result);
        }

        private bool IsCentsPartValid(string input)
        {
            return input.Split(',')[1].Length > 2;
        }

        private bool IsDollarPartValid(string input)
        {
            return input.Split(',')[0].Length > 9;
        }
    }
}
