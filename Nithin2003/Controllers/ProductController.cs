using Microsoft.AspNetCore.Mvc;
using Nithin2003.Database;
using Nithin2003.Interfaces;
using Nithin2003.Models;

namespace Nithin2003.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ApplicationData _db;

        public ProductController(IProductService productService, ApplicationData db)
        {
            _productService = productService;
            _db = db;
        }

        public IActionResult Index()
        {
            var user = _db.Users.Find(HttpContext.Session.GetString("Username"));
            var cartItems = _db.Cart.Where(c => c.Username == user.Username && c.Status == "Added To Cart").ToList();
            ViewBag.TotalProductss = cartItems.Sum(c => c.Quantity);

            
            var products = _productService.GetProducts(); // Fetch product list
            return View(products);
        }
    }
}
