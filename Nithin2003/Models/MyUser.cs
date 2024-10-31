using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class MyUser
    {
        [Key]
        public string Username { get; set; } 
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }        
        public long Mobile { get; set; }
        public string City { get; set; }
        public bool Nationality { get; set; }
        public DateTime Age { get; set; }
        public int AccountBalance { get; set; } = 0;
        public string UserStatus { get; set; } = "New";
        public bool Admin { get; set; } = false;

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]        
        public string Email { get; set; }
        public string EmailVerification { get; set; } = "NotVerified";
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool ContentEditor { get; set; } = false;

    }
}
