using Microsoft.AspNetCore.Mvc;
using Nithin2003.Database;
using Nithin2003.Models;


namespace Nithin2003.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationData _db;
        public AdminController(ApplicationData db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            try
            {
                IEnumerable<MyUser> users =_db.Users;
                return View(users);
               
            }
            catch(Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}
