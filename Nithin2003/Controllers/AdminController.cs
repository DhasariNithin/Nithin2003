using Microsoft.AspNetCore.Mvc;

namespace Nithin2003.Controllers
{
    public class AdminController : Controller
    {
        // this is for Master admin page
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}
