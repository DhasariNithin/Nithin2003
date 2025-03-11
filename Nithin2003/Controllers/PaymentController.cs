using Microsoft.AspNetCore.Mvc;
using Nithin2003.Models;

namespace Nithin2003.Controllers
{
    public class PaymentController : Controller
    {        
       private const string CASHFREE_FORM_URL = "https://payments.cashfree.com/forms?code=Nithinn";

        // Loads the Payment page
        public IActionResult Index()
        {
            return View(new PaymentViewModel());  // Sends an empty model to the View
        }

        [HttpPost]
        public IActionResult ProceedToPay(PaymentViewModel model)
        {
            if (model.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Invalid Amount");
                return View("Index", model);
            }

            // Redirect user to Cashfree Payment Form with amount
            string redirectUrl = $"{CASHFREE_FORM_URL}&amount={model.Amount}";
            return Redirect(redirectUrl);
            
        }
    }
}
