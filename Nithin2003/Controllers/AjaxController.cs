using Microsoft.AspNetCore.Mvc;
using Nithin2003.Models;

namespace Nithin2003.Controllers
{

   
    public class AjaxController : Controller
    {

        // ajax calling for live updates
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        [HttpPost]
        public int Add(int number1,int number2)
        {
            
           return number1 + number2;
        }

        // calculating the values

        [HttpPost]
        public Number Calculator(int number1, int number2)
        {
            Number number = new Number();
            number.Add = number1 + number2;
            number.Substract = number1 - number2;
            number.Multiply =number1 * number2;
            number.Divide =(decimal)number1 / number2;
            return number;
        }
    }
}
