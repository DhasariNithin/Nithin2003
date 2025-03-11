using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class ShippingDetails
    {
        
       
        public string Username { get; set; }
        public long Mobile {  get; set; }
        public string Address { get; set; }
        [Key]
        public string Email { get; set; }
        


    }
}
