using System;
using System.ComponentModel.DataAnnotations;

namespace Rubi.Dtos
{
    public class RegisterFormDto
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        //public ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
