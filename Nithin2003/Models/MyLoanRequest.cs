using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class MyLoanRequest
    {
        [Key]
        public string LoanId { get; set; }
        public string RequestedUsername { get; set; }
        public int LoanAmount { get; set; }
        public string UserComment { get; set; } 
        public DateTime LoanRequestedDate { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;
        public string AdminComment { get;set; } = "";

        public string LoanRequestStatus { get; set; } = "Pending";

    }
}
