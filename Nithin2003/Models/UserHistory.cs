using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class UserHistory
    {
        [Key]
        public string UserName { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
    }
}
