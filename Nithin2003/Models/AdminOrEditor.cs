using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class AdminOrEditor
    {
        [Key]
        public string UserName { get; set; }
        public string AdminOrContentEditor { get; set; }         
    }
}
