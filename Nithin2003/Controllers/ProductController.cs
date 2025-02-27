using Microsoft.AspNetCore.Mvc;
using Nithin2003.Interfaces;

namespace Nithin2003.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetProducts(); // Fetch product list
            return View(products);
        }
    }
}
