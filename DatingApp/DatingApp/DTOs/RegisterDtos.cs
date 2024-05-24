using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTOs
{
    public class RegisterDtos
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
