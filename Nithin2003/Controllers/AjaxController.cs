using Microsoft.AspNetCore.Mvc;
using Nithin2003.Models;

namespace Nithin2003.Controllers
{
    public class AjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
           PersonModel person = new PersonModel();
            {
                person.Name = name;
                person.DataTime = DateTime.Now.ToString();
            }
            return Json(person);
        }
    }
}
