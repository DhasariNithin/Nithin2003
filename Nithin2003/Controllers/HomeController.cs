using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nithin2003.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Nithin2003.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Contact form with email
        public IActionResult ContactUs()
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
        public IActionResult ContactUs(ContactFormModel model)
        {
            try
            {               

                if (model.Email.IsNullOrEmpty())
                {
                    ModelState.AddModelError("Email", "Email Can't Be Empty");
                    return View(model);
                }
                else
                {
                    if (model.Mobile.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Mobile", "Mobile Can't Be Empty");
                        return View(model);

                    }                    

                    ContactFormModel contactUs = new ContactFormModel();

                    contactUs.Email = "testing7702738@gmail.com";
                    contactUs.Password = "wbaagdmwlqqmfxqc";
                    contactUs.Subject = model.Subject;
                    contactUs.Mobile = model.Mobile;
                    contactUs.ToEmail = "dhasarinithin7702@gmail.com";
                    contactUs.Body = "Name : " + model.Name + "\n\n Email : " + model.Email + "\n\n Mobile No : " + model.Mobile + "\n\n Message : " + model.Body;


                    using (MailMessage mm = new MailMessage(contactUs.Email, contactUs.ToEmail))
                    {
                        mm.Subject = contactUs.Subject;
                        mm.Body = contactUs.Body;
                        mm.IsBodyHtml = false;
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            NetworkCredential NetworkCred = new NetworkCredential(contactUs.Email, contactUs.Password);
                            smtp.UseDefaultCredentials = false;
                            smtp.EnableSsl = true;
                            smtp.Host = "smtp.gmail.com";
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            ViewBag.Message = "Email sent";
                        }
                    }

                    return View();
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }

        // This is for homepage contact us form sumbission
        [HttpPost]
        public IActionResult HomepageContactUs(ContactFormModel model)
        {
            try
            {

                if (model.Email.IsNullOrEmpty())
                {
                    ModelState.AddModelError("Email", "Email Can't Be Empty");
                    return View(model);
                }
                else
                {
                    if (model.Mobile.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Mobile", "Mobile Can't Be Empty");
                        return View(model);

                    }

                    ContactFormModel contactUs = new ContactFormModel();

                    contactUs.Email = "testing7702738@gmail.com";
                    contactUs.Password = "wbaagdmwlqqmfxqc";
                    contactUs.Subject = model.Subject;
                    contactUs.Mobile = model.Mobile;
                    contactUs.ToEmail = "dhasarinithin7702@gmail.com";
                    contactUs.Body = "Name : " + model.Name + "\n\n Email : " + model.Email + "\n\n Mobile No : " + model.Mobile + "\n\n Message : " + model.Body;


                    using (MailMessage mm = new MailMessage(contactUs.Email, contactUs.ToEmail))
                    {
                        mm.Subject = contactUs.Subject;
                        mm.Body = contactUs.Body;
                        mm.IsBodyHtml = false;
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            NetworkCredential NetworkCred = new NetworkCredential(contactUs.Email, contactUs.Password);
                            smtp.UseDefaultCredentials = false;
                            smtp.EnableSsl = true;
                            smtp.Host = "smtp.gmail.com";
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            ViewBag.Message = "Email sent";
                        }
                    }

                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Errors()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}           