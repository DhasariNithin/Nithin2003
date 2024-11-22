
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nithin2003.Database;
using Nithin2003.Models;
using System.Net;
using System.Net.Mail;

namespace Nithin2003.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationData _db;
        public UsersController(ApplicationData db)
        {
            _db = db;
        }

        // reading the users data 
        public IActionResult Index()
        {
            try
            {
                IEnumerable<MyUser> users = _db.Users;
                return View(users);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // Add User Details page
        public IActionResult AddDetails()
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
        // Adding user details to SQL using form post from page
        [HttpPost]
        public IActionResult AddDetails(MyUser users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (users.Username.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Username", "Username can't be Empty");
                        return View(users);
                    }
                    else
                    {
                        if (users.Password == null)
                        {
                            ModelState.AddModelError("Password", "Password Can't be Empty");
                            return View(users);
                        }
                        if (users.Firstname.IsNullOrEmpty())
                        {
                            ModelState.AddModelError("Firstname", "Firstname Can't be Empty");
                            return View(users);
                        }
                        if (users.Lastname.IsNullOrEmpty())
                        {
                            ModelState.AddModelError("Lastname", "Lastname Can't be Empty");
                            return View(users);
                        }
                        if (users.Mobile.ToString().IsNullOrEmpty())
                        {
                            ModelState.AddModelError("Mobile", "Mobile Can't be Empty");
                            return View(users);
                        }
                        if (users.City.IsNullOrEmpty())
                        {
                            ModelState.AddModelError("City", "City Can't be Empty");
                            return View(users);
                        }
                        if (users.Nationality.ToString().IsNullOrEmpty())
                        {
                            ModelState.AddModelError("Nationality", "Nationality Can't be Empty");
                            return View(users);
                        }

                        if (users.Age.ToString().IsNullOrEmpty())
                        {
                            ModelState.AddModelError("Age", "Age Can't be Empty");
                            return View(users);
                        }
                        if (users.AccountBalance.ToString().IsNullOrEmpty())
                        {
                            ModelState.AddModelError("AccountBalance", "AccountBalance Can't be Empty");
                            return View(users);
                        }
                        if (users.UserStatus.IsNullOrEmpty())
                        {
                            ModelState.AddModelError("UserStatus", "UserStatus Can't be Empty");
                            return View(users);
                        }
                        if (users.Email.IsNullOrEmpty())
                        {
                            ModelState.AddModelError("Email", "Email Can't be Empty");
                            return View(users);
                        }
                        if (users.EmailVerification.IsNullOrEmpty())
                        {
                            ModelState.AddModelError("EmailVerification", "EmailVerification Can't be Empty");
                            return View(users);
                        }
                        if (users.LastModifiedDate.ToString().IsNullOrEmpty())
                        {
                            ModelState.AddModelError("LastModifiedDate", "LastModifiedDate Can't be Empty");
                            return View(users);
                        }
                        var isEmailAlreadyExistss = _db.Users.Any(x => x.Email == users.Email);
                        if (isEmailAlreadyExistss)
                        {
                            ModelState.AddModelError("Email", "User with this email already exists please try with another Email");
                            return View(users);
                        }
                        var UsernameAlreadyExists = _db.Users.Any(x => x.Username == users.Username);
                        if (UsernameAlreadyExists)
                        {
                            ModelState.AddModelError("Username", "User with this User Name already exists please try with another User Name");
                            return View(users);
                        }

                        _db.Users.Add(users);
                        _db.SaveChanges();
                        return RedirectToAction("SignIn", "Users");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // Delete User Details using asp-route
        public IActionResult deleteDetails(string? Username)
        {
            try
            {
                var user = _db.Users.Find(Username);
                _db.Users.Remove(user);
                _db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // Update User Details 
        public IActionResult EditDetails(string? Username)
        {
            try
            {
                var users = _db.Users.Find(Username);
                return View(users);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        [HttpPost]
        public IActionResult EditDetails(MyUser users)
        {
            try
            {
                if (users.Username.IsNullOrEmpty())
                {
                    ModelState.AddModelError("Username", "Username Can't be Empty");
                    return View(users);
                }
                else
                {
                    if (users.Password == null)
                    {
                        ModelState.AddModelError("Password", "Password Can't be Empty");
                        return View(users);
                    }
                    if (users.Firstname.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Firstname", "Password Can't be Empty");
                        return View(users);
                    }
                    if (users.Lastname.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Lastname", "Lastname Can't be Empty");
                        return View(users);
                    }
                    if (users.Mobile.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Mobile", "Mobile Can't be Empty");
                        return View(users);
                    }
                    if (users.City.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("City", "City Can't be Empty");
                        return View(users);
                    }
                    if (users.Nationality.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Nationality", "Nationality Can't be Empty");
                        return View(users);
                    }

                    if (users.Age.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Age", "Age Can't be Empty");
                        return View(users);
                    }
                    if (users.AccountBalance.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("AccountBalance", "AccountBalance Can't be Empty");
                        return View(users);
                    }
                    if (users.UserStatus.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("UserStatus", "UserStatus Can't be Empty");
                        return View(users);
                    }
                    if (users.Email.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("Email", "Email Can't be Empty");
                        return View(users);
                    }
                    if (users.EmailVerification.IsNullOrEmpty())
                    {
                        ModelState.AddModelError("EmailVerification", "EmailVerification Can't be Empty");
                        return View(users);
                    }
                    if (users.LastModifiedDate.ToString().IsNullOrEmpty())
                    {
                        ModelState.AddModelError("LastModifiedDate", "LastModifiedDate Can't be Empty");
                        return View(users);
                    }
                    _db.Users.Update(users);
                    _db.SaveChanges();
                    if (users.Admin == true)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (users.ContentEditor == true)
                    {
                        return RedirectToAction("UsersContentEditor", "Users");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }
        // User Sign In
        public IActionResult SignIn()
        {
            try
            {
                if (HttpContext.Session.GetString("SignIn") == "True")
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // validating the user with credentials

        [HttpPost]
        public IActionResult SignIn(MySignIn mySignIn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _user = _db.Users.Find(mySignIn.Username);
                    if (_user != null)
                    {
                        if (mySignIn.Password == _user.Password)
                        {

                            HttpContext.Session.SetString("Username", _user.Username);
                            HttpContext.Session.SetString("SignIn", "True");

                            UserHistory history = new UserHistory();
                            if (HttpContext.Session.GetString("SignIn") == "True")
                            {
                                history.UserName = _user.Username;
                                history.Action = "SignIn";
                                history.Time = DateTime.Now;

                                IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                                string result = "";
                                if (remoteIpAddress != null)
                                {
                                    // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                                    // This usually only happens when the browser is on the same machine as the server.
                                    if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                                    {
                                        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
                                .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                                    }
                                    result = remoteIpAddress.ToString();
                                }
                                history.IPAddress = result;
                                _db.LoginHistory.Add(history);
                                _db.SaveChanges();

                            }
                            if (_user.Username == "Manu")
                            {
                                HttpContext.Session.SetString("Manu", "True");
                            }

                            if (_user.UserStatus == "Suspend")
                            {
                                HttpContext.Session.SetString("UserStatus", "Suspend");
                            }
                            if (_user.UserStatus == "New")
                            {
                                HttpContext.Session.SetString("UserStatus", "New");
                            }
                            if (_user.EmailVerification == "Verified")
                            {
                                HttpContext.Session.SetString("EmailVerification", "True");
                            }
                            if (_user.ContentEditor)
                            {
                                HttpContext.Session.SetString("ContentEditor", "True");
                            }
                            if (_user.Admin)
                            {

                                HttpContext.Session.SetString("Admin", "True");
                                return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Dashboard");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Password doesn't match, please try with a valid password");
                            return View();
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Username doesn't exist, please try with a valid username");
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // User sign-out amd remove seesions
        public IActionResult SignOut()
        {
            try
            {
                var _user = _db.Users.Find(HttpContext.Session.GetString("Username"));


                HttpContext.Session.SetString("Username", "");
                HttpContext.Session.SetString("SignIn", "False");              

                HttpContext.Session.SetString("ContentEditor", "False");
                HttpContext.Session.SetString("Admin", "False");
                HttpContext.Session.SetString("EmailVerification", "False");
                HttpContext.Session.SetString("UserStatus", "");
                HttpContext.Session.SetString("Manu", "False");

                return RedirectToAction("SignIn", "Users");
            }


            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // Suspend the user
        public IActionResult UserSuspend(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);
                _user.UserStatus = "Suspend";
                _db.Users.Update(_user);
                _db.SaveChanges();

                if (HttpContext.Session.GetString("Admin") == "True")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("UsersContentEditor", "Users");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        // unsuspend the user
        public IActionResult UserUnSuspend(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);

                if (_user.UserStatus == "Suspend")
                {
                    _user.UserStatus = "Active";
                    HttpContext.Session.SetString("UserStatus", "Active");

                }
                _db.Users.Update(_user);
                _db.SaveChanges();
                if (HttpContext.Session.GetString("Admin") == "True")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("UsersContentEditor", "Users");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        // Approving the user to active status
        public IActionResult UserApprove(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);

                if (_user.UserStatus == "New")
                {
                    _user.UserStatus = "Active";
                    HttpContext.Session.SetString("UserStatus", "Active");
                }
                _db.Users.Update(_user);
                _db.SaveChanges();

                if (HttpContext.Session.GetString("Admin") == "True")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("UsersContentEditor", "Users");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        // forgot password sending password to email 
        public IActionResult ForgotPassword()
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
        public IActionResult ForgotPassword(ForgetPassword forgetPassword)
        {
            try
            {

                var _userObject = _db.Users.Find(forgetPassword.Username);
                if (_userObject == null)
                {
                    ModelState.AddModelError("Username", "User does not exist, please enter valid Username");
                    return View();
                }
                if (_userObject != null)
                {
                    if (forgetPassword.Username == _userObject.Username)
                    {
                        // sending an email
                        ContactFormModel contactUs = new ContactFormModel();

                        contactUs.Email = "testing7702738@gmail.com";
                        contactUs.Password = "wbaagdmwlqqmfxqc";
                        contactUs.ToEmail = _userObject.Email;
                        contactUs.Body = "User Name = " + _userObject.Username + "\n\n Password  = " + _userObject.Password;


                        using (MailMessage mm = new MailMessage(contactUs.Email, contactUs.ToEmail))
                        {
                            //mm.Subject = contactUs.Subject;
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
                                ViewBag.Text = "Password has been sent to your registered email successfully.";
                            }
                        }
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "User Name does not match with user details please try again");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("Username", "Username Can't be empty ");
                    return View();
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        public IActionResult UsersContentEditor()
        {
            try
            {
                IEnumerable<MyUser> users = _db.Users;
                return View(users);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

    }
}
