using System.ComponentModel.DataAnnotations;

namespace Rubi.Dtos
{
    public class LoginFormDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
