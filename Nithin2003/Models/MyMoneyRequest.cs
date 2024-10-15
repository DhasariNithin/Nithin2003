﻿using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class MyMoneyRequest
    {
        [Key]
        public int LoanId { get; set; }
        public string RequestedUsername { get; set; }
        public int LoanAmount { get; set; }
        public string UserComment { get; set; } = "Reqesting Loan ";
        public DateTime LoanRequestedDate { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;
        public string AdminComment { get;set; } = "";
    }
}
