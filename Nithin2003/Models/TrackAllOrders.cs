﻿using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class TrackAllOrders
    {
        [Key]
        public int PaymentId { get; set; }

        public string OrderStatus { get; set; } = "";

        public int Amount { get; set; }

        public DateTime StatusUpdated { get; set; } = DateTime.Now;
    }
}
