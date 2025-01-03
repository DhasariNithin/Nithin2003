﻿using Microsoft.AspNetCore.Mvc;
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

        // Read all users 
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
        // checking users transaction details
        public IActionResult UserstransactionHistory()
        {
            try
            {
                IEnumerable<MyTransferMoney> myTransferMoney = _db.TransactionHistory;
                return View(myTransferMoney);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }
        // Checking Users loan requests 
        public IActionResult LoanAmountRequest()
        {
            try
            {
                IEnumerable<MyLoanRequest> myloanrequest = _db.LoanRequest;
                return View(myloanrequest);


            }

            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }
        //Approve Loan button
        public IActionResult ApproveLoan(string? LoanId)
        {
            try
            {
                // fetching loan request details from Loan request table using Load ID
                var _requesteduser = _db.LoanRequest.Find(LoanId);

                // fetching user details
                var _user = _db.Users.Find(_requesteduser.RequestedUsername);
                _user.AccountBalance = _user.AccountBalance + _requesteduser.LoanAmount;// adding loan amount to user amount
                _db.Users.Update(_user);

                // update loan request table with status change and last modified

                _requesteduser.LoanRequestStatus = "Approved";
                _requesteduser.LastModified = DateTime.Now;
                _requesteduser.AdminComment = "Loan approved";

                _db.LoanRequest.Update(_requesteduser);
                _db.SaveChanges();

                return RedirectToAction("LoanAmountRequest");


            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // Reject loan button
        public IActionResult RejectLoan(string? LoanId)
        {
            try
            {

                var _requesteduser = _db.LoanRequest.Find(LoanId);

                //Updating Loan Request Table
                _requesteduser.LoanRequestStatus = "Rejected";
                _requesteduser.LastModified = DateTime.Now;
                _requesteduser.AdminComment = "Loan Rejected";

                _db.LoanRequest.Update(_requesteduser);
                _db.SaveChanges();
                return RedirectToAction("LoanAmountRequest");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
        // Page for aprove admin or editors
        public IActionResult ApproveAsAdmin()
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

        // giving permission to user admin or editor
        public IActionResult ApproveAsAdmin(AdminOrEditor AdminOrEditor)
        {
            try
            {


                var _usersdata = _db.Users.Find(AdminOrEditor.UserName);
                if (_usersdata == null)
                {
                    ModelState.AddModelError("UserName", "User Name doesn't exist");
                    return View();
                }
                if (_usersdata.UserStatus == "New") 
                {
                    ModelState.AddModelError("UserName", "User account has not approvedyou can't make this user as admin or Content Editor");
                    return View();
                }
                if (_usersdata.UserStatus == "Suspend")
                {
                    ModelState.AddModelError("UserName", "User account has been suspended you can't make this user as admin or Content Editor");
                    return View();
                }

                if (_usersdata != null)
                {
                    if (_usersdata.UserStatus == "Active")
                    {

                        if (AdminOrEditor.AdminOrContentEditor == "Admin")
                        {
                            _usersdata.Admin = true;
                            _usersdata.ContentEditor = false;
                        }
                        else if (AdminOrEditor.AdminOrContentEditor == "ContentEditor")
                        {
                            _usersdata.ContentEditor = true;
                            _usersdata.Admin = false;
                        }
                        else
                        {
                            _usersdata.ContentEditor = false;
                            _usersdata.Admin = false;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "You can't make this user as admin or Content Editor");
                        return View();
                    }
                    

                    _db.Users.Update(_usersdata);
                    _db.SaveChanges();
                    ViewBag.Message = "Successfully permissions given to User.";
                    return View();


                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // Reading admin and editors details
        public IActionResult AdminAndEditor()
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
        
        // removing permissions as admin
        public IActionResult RemoveAsAdmin(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);
                if(_user.Admin == true)
                {
                    _user.Admin = false;
                }
                _db.Users.Update(_user);
                _db.SaveChanges();
                return RedirectToAction("AdminAndEditor", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }

        // removing permissions as Editor
        public IActionResult RemoveAsEditor(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);
                if (_user.ContentEditor == true)
                {
                    _user.ContentEditor = false;
                }
                _db.Users.Update(_user);
                _db.SaveChanges();
                return RedirectToAction("AdminAndEditor", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}
