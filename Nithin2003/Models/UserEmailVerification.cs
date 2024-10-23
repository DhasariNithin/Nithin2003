using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class UserEmailVerification
    {
      
        public string Email { get; set; }
        [Key]
        public string Username { get; set; }
       
        public int OTP { get; set; }
        public DateTime OTPValidity { get; set; }
    }
}
