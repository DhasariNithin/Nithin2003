using Microsoft.AspNetCore.Mvc;
using Nithin2003.Database;
using Nithin2003.Models;
using System.Net.Mail;
using System.Net;

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
        public IActionResult UserLoanRequest()
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
        public IActionResult UserLoanRequest(MyLoanRequest Request)
        {

            try
            {
                Request.RequestedUsername = HttpContext.Session.GetString("Username");

                Random rand = new Random();
                Request.LoanId = rand.Next() + Request.RequestedUsername + Request.LoanAmount;
                if(Request.UserComment == null)
                {
                    ModelState.AddModelError("UserComment", "UserComment Can't be Empty");
                    return View();
                }
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
        public IActionResult TrackLoanRequest()
        {
            try
            {
                IEnumerable<MyLoanRequest> myloanrequest = _db.LoanRequest.Where(t => t.RequestedUsername.Contains(HttpContext.Session.GetString("Username")));
                return View(myloanrequest);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }

        public IActionResult VerifyEmail()
        {
            try
            {
                

                var _user = _db.Users.Find(HttpContext.Session.GetString("Username"));

                UserEmailVerification _emailVerification = new UserEmailVerification();
                _emailVerification.Email = _user.Email;
                _emailVerification.Username = _user.Username;

                return View(_emailVerification);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }
        //genrating OTP for email verification

        public IActionResult SendOTP(string? Username)
        {
            try
            {
                var _user = _db.Users.Find(Username);
                // creating an object for the same
                UserEmailVerification _emailVerification = new UserEmailVerification();
                Random random = new Random();


                var _isOTPAlready = _db.EmailVerification.Find(Username);
                _emailVerification.Email = _user.Email;
                _emailVerification.Username = Username;

                if (_isOTPAlready != null)
                {
                    _isOTPAlready.OTP = random.Next(0, 1000000);
                    _isOTPAlready.OTPValidity = DateTime.Now;
                }
                else
                {
                    _emailVerification.OTP = random.Next(0, 1000000);
                    _emailVerification.OTPValidity = DateTime.Now;
                }




                // sending an email
                ContactFormModel contactUs = new ContactFormModel();

                contactUs.Email = "testing7702738@gmail.com";
                contactUs.Password = "wbaagdmwlqqmfxqc";
                contactUs.Subject = "Your Email verifcation OTP";

                contactUs.ToEmail = _emailVerification.Email;
                contactUs.Body = "Your Email verifcation OTP is " + _emailVerification.OTP;


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
                        //ViewBag.Text = "OTP sent to your email successfully";
                    }
                }

                HttpContext.Session.SetString("OTPGenerated", "True");


                // saving OTP details to table
                if (_isOTPAlready == null)
                {
                    _db.EmailVerification.Add(_emailVerification);
                }
                else
                {
                    _db.EmailVerification.Update(_isOTPAlready);
                }

                _db.SaveChanges();
                return RedirectToAction("VerifyEmail", "Dashboard");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }

        }

        [HttpPost]
        public IActionResult VerifyEmail(UserEmailVerification enteredDetails)
        {
            try
            {

                var _user = _db.Users.Find(HttpContext.Session.GetString("Username"));
                var _dbOTPDetails = _db.EmailVerification.Find(_user.Username);
                

                if (_dbOTPDetails.OTP == enteredDetails.OTP)
                {
                    var timeSpan = DateTime.Now - _dbOTPDetails.OTPValidity;

                    if (timeSpan.TotalSeconds > 180)
                    {
                        ModelState.AddModelError("OTP", "OTP expired, please request new OTP, new OTP is valid for 3 minutes only");
                        return View();                        
                    }



                    _user.EmailVerification = "Verified";
                    _db.Users.Update(_user);
                    _db.SaveChanges();
                    HttpContext.Session.SetString("EmailVerification", "True");
                    ViewBag.messageOtp = "OTP Verified Sucessfully";

                    if (HttpContext.Session.GetString("EmailVerification") == "True")
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("OTP", "OTP does not match, please enter correct OTP");                    
                    return View();
                }

                return View();




            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Home");
            }
        }
    }
}








