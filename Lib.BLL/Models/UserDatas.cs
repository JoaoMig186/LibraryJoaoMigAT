using System.ComponentModel.DataAnnotations;

namespace Lib.BLL.Models
{
    public class UserDatas
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
