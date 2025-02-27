using Microsoft.AspNetCore.Mvc;
using Nithin2003.Database;
using Nithin2003.Interfaces;
using Nithin2003.Models;
using Nithin2003.Services;

namespace Nithin2003.Controllers
{
    public class CartController : Controller
    {

        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ApplicationData _db;

        public CartController(IProductService productService, IUserService userService, ApplicationData db)
        {
            _productService = productService;
            _userService = userService;
            _db = db;
        }

        public IActionResult Index()
        {
            var user = _userService.GetUser(HttpContext.Session.GetString("Username"));
            ViewBag.UserBalance = user?.AccountBalance ?? 0;

            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");
            if (cart != null)
            {
                ViewBag.total = cart.Sum(s => s.Quantity * s.Product.Price);
            }
            else
            {
                cart = new List<Product_Item>();
                ViewBag.total = 0;
            }

            return View(cart);
        }

        public IActionResult Buy(string sku)
        {
            var product = _productService.GetProduct(sku);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            if (cart == null) //no item in the cart
            {
                cart = new List<Product_Item>();
                cart.Add(new Product_Item { Product = product, Quantity = 1 });
            }
            else
            {
                int index = cart.FindIndex(w => w.Product.Sku == sku);

                if (index != -1) //if item already in the 
                {
                    cart[index].Quantity++; //increment by 1
                }
                else
                {
                    cart.Add(new Product_Item { Product = product, Quantity = 1 });
                }
            }

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Add(string sku)
        {
            var product = _productService.GetProduct(sku);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            int index = cart.FindIndex(w => w.Product.Sku == sku);
            cart[index].Quantity++; //increment by 1

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Minus(string sku)
        {
            var product = _productService.GetProduct(sku);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            int index = cart.FindIndex(w => w.Product.Sku == sku);

            if (cart[index].Quantity == 1) //last item of a product
            {
                cart.RemoveAt(index); //remove it
            }
            else
            {
                cart[index].Quantity--; //reduce by 1
            }


            HttpContext.Session.Set<List<Product_Item>>("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string sku)
        {
            var product = _productService.GetProduct(sku);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            int index = cart.FindIndex(w => w.Product.Sku == sku);
            cart.RemoveAt(index);

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);

            return RedirectToAction("Index");
        }
        public IActionResult Pay(MyUser _user)
        {
            var _userDetails = _db.Users.Find(HttpContext.Session.GetString("Username"));

            Payment _payment = new Payment();
            Product _product = new Product();
            Product_Item _Item = new Product_Item();
            List<Product>products = new List<Product>();

            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");
            
                var amountt = cart.Sum(s => s.Quantity * s.Product.Price);
            

            // Random number generator 
            Random rand = new Random();
            _payment.PaymentId = rand.Next(0, 1000000000);
            _payment.Username = _userDetails.Username;
            _payment.Amount = (int)amountt;
            _payment.PaymentDate = DateTime.Now;
            if( _userDetails.AccountBalance < amountt)
            {
                return RedirectToAction("ErrorAmount", "Home");
            }
            if (_userDetails.AccountBalance <= 0 || _userDetails.AccountBalance  < _payment.Amount)
            {
                return RedirectToAction("ErrorAmount", "Home");
            }


            _userDetails.AccountBalance = _userDetails.AccountBalance - _payment.Amount;
            //ViewBag.Message = _payment.PaymentId;
            _db.PaymentRecords.Add(_payment);
            _db.Users.Update( _userDetails );
            _db.SaveChanges();
            return RedirectToAction("Success","Cart");
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult TrackOrder()
        {
            //IEnumerable<Payment> userPayments = _db.PaymentRecords.Where(p => p.Username == (HttpContext.Session.GetString("Username")));

            IEnumerable<Payment> _payment = _db.PaymentRecords.Where(t => t.Username.Contains(HttpContext.Session.GetString("Username")));
            return View(_payment);
        }
    }
}
