using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : Controller
    {
        // Сложение
        [HttpGet("add")]
        public ActionResult<double> Add(double a, double b)
        {
            return Ok(a + b);
        }

        // Вычитание
        [HttpGet("subtract")]
        public ActionResult<double> Subtract(double a, double b)
        {
            return Ok(a - b);
        }

        // Умножение
        [HttpGet("multiply")]
        public ActionResult<double> Multiply(double a, double b)
        {
            return Ok(a * b);
        }

        // Деление
        [HttpGet("divide")]
        public ActionResult<double> Divide(double a, double b)
        {
            if (b == 0)
                return BadRequest("Деление на ноль невозможно.");
            return Ok(a / b);
        }
    }
}
