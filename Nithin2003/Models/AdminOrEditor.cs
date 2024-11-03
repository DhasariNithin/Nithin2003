using System.ComponentModel.DataAnnotations;

namespace Nithin2003.Models
{
    public class AdminOrEditor
    {
        [Key]
        public string UserName { get; set; }
        public bool Admin { get; set; } =false;
        public bool Contenteditor { get; set; } = false;
    }
}
