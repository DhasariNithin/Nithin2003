using Microsoft.AspNetCore.Mvc;
using Nithin2003.Models;
using Razorpay.Api;

namespace Nithin2003.Controllers
{
    public class MyOrderController : Controller
    {
        [BindProperty]
        public EntityOrder _orderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        // Creating order with amount, transaction Id and adding razor pay details of key and secret
        public ActionResult CreateOrder()
        {
            string key = "  ";
            string secret = "";

            Random _random = new Random();
            string TransactionId = _random.Next(0, 3000).ToString();

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(_orderDetails.Amount) * 100); // Amount is in currency subunits. Default currency is INR. Hence, 50000 refers to 50000 paise
            input.Add("currency", "INR");
            input.Add("receipt", TransactionId);

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderId = order["id"].ToString();
            return View("Payment", _orderDetails);
        }
        // adding payment details
        public ActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            Dictionary<string, string> Attributes = new Dictionary<string, string>();
            Attributes.Add("razorpay_order_id", razorpay_order_id);
            Attributes.Add("razorpay_payment_id", razorpay_payment_id);
            Attributes.Add("razorpay_signature", razorpay_signature);

            EntityOrder _entityorderss = new EntityOrder();
            _entityorderss.TransactionId = razorpay_payment_id;
            _entityorderss.OrderId = razorpay_order_id;

            Utils.verifyPaymentSignature(Attributes);
            return View("PaymentSucess", _entityorderss);

        }
    }
}
