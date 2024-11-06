using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class ForgotUsername
    {
        [Key]
        [Required]
        public string Email { get; set; }
    }
}
