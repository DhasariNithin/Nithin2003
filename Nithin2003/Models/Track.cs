using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
        public class Track
        {
        [Key]
            public int OrderId { get; set; }
            public string Status { get; set; }
            public string username { get; set; }
            public DateTime Date { get; set; }
        }
}
