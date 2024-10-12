namespace Nithin2003.Models
{
    public class MyMoneyRequest
    {
        public int LoanId { get; set; }
        public string RequestedUsername { get; set; }
        public int LoanAmount { get; set; }
        public string UserComment { get; set; }
        public DateOnly LoanRequestedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string AdminComment { get;set; }
    }
}
