using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class ForgetPassword
    {

        [Key]
        [Required]
        public string Username { get; set; }
    }
}
