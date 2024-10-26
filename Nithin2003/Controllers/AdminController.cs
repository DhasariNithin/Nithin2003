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

                return RedirectToAction("LoanAmountRequest");


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
                return RedirectToAction("LoanAmountRequest");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}
