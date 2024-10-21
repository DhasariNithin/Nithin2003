using System;
using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Password { get; set; }
        public long Mobile { get; set; }
        public string ToEmail { get; set; }

    }
}
