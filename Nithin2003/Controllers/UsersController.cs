﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nithin2003.Database;
using Nithin2003.Models;
using System.Net.Http;

namespace Nithin2003.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationData _db;
        public UsersController(ApplicationData db)
        {
            _db = db;
        }

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

        // Add User Details
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
        [HttpPost]
        public IActionResult AddDetails(MyUser users)
        {
            try
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

                    _db.Users.Add(users);


                    _db.SaveChanges();
                    return RedirectToAction("Index", "Users");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        // Delete User Details
        public IActionResult deleteDetails(string? Username)
        {
            try
            {
                var user = _db.Users.Find(Username);
                _db.Users.Remove(user);
                _db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
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
                    return RedirectToAction("Index", "Admin");
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
                                var IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                                history.IPAddress = IpAddress;
                                _db.LoginHistory.Add(history);
                                    _db.SaveChanges();                               
                                   
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
                UserHistory history = new UserHistory();
                if (HttpContext.Session.GetString("SignIn") == "False")
                {
                    history.UserName = _user.Username;
                    history.Action = "SignOut";
                    history.Time = DateTime.Now;
                    var IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    history.IPAddress = IpAddress;
                    _db.LoginHistory.Add(history);
                    _db.SaveChanges();

                }
                HttpContext.Session.SetString("Admin", "False");
                HttpContext.Session.SetString("EmailVerification", "False");
                HttpContext.Session.SetString("UserStatus", "");
                return RedirectToAction("SignIn", "Users");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        public IActionResult UserSuspend(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);
                _user.UserStatus = "Suspend";
                _db.Users.Update(_user);
                _db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
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
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
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
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}
