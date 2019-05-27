using System.ComponentModel.DataAnnotations;

namespace Rubi.Dtos
{
    public class LoginFormDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
