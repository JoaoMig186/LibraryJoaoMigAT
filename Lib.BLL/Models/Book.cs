using System.ComponentModel.DataAnnotations;

namespace Lib.BLL.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Code { get; set; }

    }
}
