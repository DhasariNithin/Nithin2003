using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class MyTransferMoney
    {
       
        public string FromUsername { get; set; }

        public string ToUsername { get; set; }

        public int Amount { get; set; }
        
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        [Key]
        public string TransactionId { get; set; } 
    }
}
