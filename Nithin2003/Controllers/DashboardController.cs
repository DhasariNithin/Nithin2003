using Microsoft.AspNetCore.Mvc;
using Nithin2003.Database;
using Nithin2003.Models;
using NuGet.Protocol.Plugins;
using System.ComponentModel;

namespace Nithin2003.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ApplicationData _db;
        public DashBoardController(ApplicationData db)
        {
            _db = db;
        }
        // User Sign In Details
        public IActionResult Index()
        {
            try
            {
                string _username = HttpContext.Session.GetString("Username");
                var _userObject = _db.Users.Find(_username);
                return View(_userObject);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }


        // Money Transfer
        public IActionResult TransferMoney()
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
        public IActionResult TransferMoney(MyTransferMoney myTransferMoney)
        {
            try
            {

                var _toUserObj = _db.Users.Find(myTransferMoney.ToUsername); // checking To Username
                if (_toUserObj == null)
                {
                    ModelState.AddModelError("ToUsername", "User doesn't exist, please enter valid username");
                    return View();
                }
                var _fromUser = _db.Users.Find(HttpContext.Session.GetString("Username"));
                if (myTransferMoney.Amount <= 0 || myTransferMoney.Amount > _fromUser.AccountBalance)
                {
                    ModelState.AddModelError("Amount", "Please enter the amount greather than zero and less than or equal to your account balance");
                    return View();
                }
                _toUserObj.AccountBalance = _toUserObj.AccountBalance + myTransferMoney.Amount;//Adding amount to ToUsername
                _fromUser.AccountBalance = _fromUser.AccountBalance - myTransferMoney.Amount;//substracting amount from fromUser
                _db.Users.Update(_toUserObj);
                _db.Users.Update(_fromUser);

                // adding transfer history to Transfer Money table

                myTransferMoney.FromUsername = _fromUser.Username;
                myTransferMoney.TransactionDate = DateTime.Now;
                // Random number generator 
                Random rand = new Random(10000);

                myTransferMoney.TransactionId = rand.Next() + _fromUser.Username + rand.Next();
                _db.TransactionHistory.Add(myTransferMoney);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }
        public IActionResult MoneyRequest()
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
        public IActionResult MoneyRequest(MyLoanRequest Request)
        {

            try
            {
                Request.RequestedUsername = HttpContext.Session.GetString("Username");

                Random rand = new Random(10000);
                Request.LoanId = rand.Next()+Request.RequestedUsername;
                _db.LoanRequest.Add(Request);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }

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

                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}
    

    

      

    
